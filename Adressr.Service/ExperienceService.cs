using Adressr.Data.Model;
using Adressr.Data.Repository.Interface;
using Adressr.Library.CustomExceptions;
using Adressr.Library.Models;
using Adressr.Service.Interface;

namespace Adressr.Service
{
    public class ExperienceService : IExperienceService
    {
        private readonly IExperienceRepository _experienceRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly IWorkTaskRepository _workTaskRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IRepositorySaver _repositorySaver;

        public ExperienceService(IExperienceRepository experienceRepository, ISkillRepository skillRepository, IWorkTaskRepository workTaskRepository, IProfileRepository profileRepository, IRepositorySaver repositorySaver)
        {
            _experienceRepository = experienceRepository;
            _skillRepository = skillRepository;
            _workTaskRepository = workTaskRepository;
            _profileRepository = profileRepository;
            _repositorySaver = repositorySaver;
        }

        private static WorkTaskResponse ToTaskResponse(WorkTask task, ILookup<int?, WorkTask> byParent)
        {
            WorkTaskResponse ToReturn = new WorkTaskResponse
            {
                WorkTaskID = task.WorkTaskID,
                Description = task.Description,
                CreatedDate = task.CreatedDate,
                CompletedDate = task.CompletedDate,
                IsDone = task.IsDone,
                SubTasks = byParent[task.WorkTaskID].OrderBy(t => t.WorkTaskID).Select(t => ToTaskResponse(t, byParent)).ToList() //Recursion, need to enforce structure. task will be parent task first time, then subtask the second, and then nothing. Need to enforce structure when adding tasks so this assumption holds.
            };
            return ToReturn;
        }

        private static SkillResponse ToSkillResponse(Skill skill)
        {
            return new SkillResponse
            {
                Name = skill.Name
            };
        }

        private async Task<List<ExperienceResponse>> BuildResponseAsync(List<Experience> experiences)
        {
            if(experiences.Count == 0)
            {
                return [];
            }

            List<int> experienceIds = experiences.Select(exp => exp.ExperienceID).ToList();
            ILookup<int, Skill> skillsByExperience = await _skillRepository.GetSkillsByExperienceIdsAsync(experienceIds);
            List<WorkTask> tasks = await _workTaskRepository.GetTaskByExperienceIdsAsync(experienceIds);
            ILookup<int, WorkTask> tasksByExperience = tasks.ToLookup(task => task.ExperienceID);

            List<ExperienceResponse> result = experiences.Select(exp =>
            {
                ILookup<int?, WorkTask> byParent = tasksByExperience[exp.ExperienceID].ToLookup(t => t.ParentTaskID);
                return new ExperienceResponse
                {
                    ExperienceID = exp.ExperienceID,
                    CompanyName = exp.CompanyName,
                    RoleTitle = exp.RoleTitle,
                    Description = exp.Description,
                    StartDate = exp.StartDate,
                    EndDate = exp.EndDate,
                    IsExported = exp.IsExported,
                    Skills = skillsByExperience[exp.ExperienceID].OrderBy(skill => skill.Name).Select(ToSkillResponse).ToList(),
                    CurrentTasks = byParent[null].Where(task => !task.IsDone).OrderBy(task => task.WorkTaskID).Select(task => ToTaskResponse(task, byParent)).ToList(), //tasksByExperience[exp.ExperienceID].Where(task => !task.IsDone).OrderBy(task => task.WorkTaskID).Select(ToTaskResponse).ToList()
                    PreviousTasks = byParent[null].Where(task => task.IsDone).OrderByDescending(task => task.CompletedDate).Select(task => ToTaskResponse(task, byParent)).ToList() //tasksByExperience[exp.ExperienceID].Where(task => task.IsDone).OrderByDescending(task => task.CompletedDate).Select(ToTaskResponse).ToList()
                };
            }).ToList();

            return result;
        }

        private static WorkTask NewTask(string description, bool isDone, CreateWorkTaskRequest task)
        {
            return new WorkTask
            {
                Description = description,
                IsDone = isDone,
                CompletedDate = DateTime.UtcNow,
                SubTasks = task.SubTasks.Select(subTask => new WorkTask
                {
                    Description = subTask.Description,
                    IsDone = subTask.IsDone,
                    CompletedDate = DateTime.UtcNow
                }).ToList()
            };
        }

        public async Task<List<ExperienceResponse>> GetExperiencesExportedByProfileIdAsync(int profileId)
        {
            List<Experience> experiences = await _experienceRepository.GetExportedByProfileIdAsync(profileId);
            return await BuildResponseAsync(experiences);
        }

        public async Task<List<ExperienceResponse>> GetExperiencesForManager(int userId)
        {
            List<Experience> results = await _experienceRepository.GetAllExperienceByUserIdAsync(userId);
            return await BuildResponseAsync(results);
        }

        public async Task<ExperienceResponse> AddExperienceAsync(int userId, CreateExperienceRequest request)
        {
            Profile? profile = await _profileRepository.GetByUserIdAsync(userId);
            if(profile == null)
            {
                throw new NotFoundException(nameof(Profile), userId);
            }
            if (profile.UserID != userId)
            {
                throw new ForbiddenException("You are not allowed to add experience for this account.", "Some text here.");
            }

            Experience experience = new Experience
            {
                CompanyName = request.CompanyName,
                RoleTitle = request.RoleTitle,
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                IsExported = false,
                Profile = profile
            };

            //List<WorkTask> previousTasks = [];
            //List<WorkTask> currentTasks = [];

            //if (request.PreviousTasks != null)
            //{
            //    //Created Date AND completed date auto-populates to DateTime.UtcNow, but a feature to add would be giving user access to input a date for an experience's tasks manually.
            //    previousTasks = request.PreviousTasks.Select(task => NewTask(task.Description, task.IsDone, task)).ToList();
            //}

            //if (request.CurrentTasks != null)
            //{
            //    currentTasks = request.CurrentTasks.Select(task => NewTask(task.Description, task.IsDone, task)).ToList();
            //}

            List<WorkTask> tasks = request.Tasks.Select(task => NewTask(task.Description, task.IsDone, task)).ToList();

            //experience.Tasks = previousTasks.Concat(currentTasks).ToList();
            experience.Tasks = tasks;

            List<Skill> skills = [];
            if (request.Skills != null)
            {
                foreach(SkillRequest skill in request.Skills)
                {
                    Skill? skillCheck = await _skillRepository.GetSkillAsync(skill.Name);
                    if(skillCheck != null)
                    {
                        experience.Skills.Add(skillCheck);
                    }
                    else
                    {
                        Skill newSkill = new Skill
                        {
                            Name = skill.Name
                        };
                        await _skillRepository.AddLaterAsync(newSkill);
                        experience.Skills.Add(newSkill);
                    }
                }
            }

            await _experienceRepository.AddAsync(experience);
            await _repositorySaver.SaveChangesAsync();
            return (await BuildResponseAsync([experience]))[0]; //BuildResponseAsync creates a list, but we just need one entry which is the first one.
        }
    }
}
