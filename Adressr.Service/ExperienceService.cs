using Adressr.Data.Model;
using Adressr.Data.Repository.Interface;
using Adressr.Library.Models;
using Adressr.Service.Interface;

namespace Adressr.Service
{
    public class ExperienceService : IExperienceService
    {
        private readonly IExperienceRepository _experienceRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly IWorkTaskRepository _workTaskRepository;

        public ExperienceService(IExperienceRepository experienceRepository, ISkillRepository skillRepository, IWorkTaskRepository workTaskRepository)
        {
            _experienceRepository = experienceRepository;
            _skillRepository = skillRepository;
            _workTaskRepository = workTaskRepository;
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

        public async Task<List<ExperienceResponse>> GetExperiencesExportedByProfileIdAsync(int profileId)
        {
            List<Experience> experiences = await _experienceRepository.GetExportedByProfileIdAsync(profileId);
            return await BuildResponseAsync(experiences);
        }
    }
}
