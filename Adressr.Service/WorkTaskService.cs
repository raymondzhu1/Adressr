using Adressr.Data.Model;
using Adressr.Data.Repository.Interface;
using Adressr.Library.Models;
using Adressr.Service.Interface;

namespace Adressr.Service
{
    public class WorkTaskService : IWorkTaskService
    {
        private readonly IExperienceRepository _experienceRepository;
        private readonly IWorkTaskRepository _workTaskRepository;

        private const int MaxTasksPerExperienceForDashboard = 5;

        public WorkTaskService(IExperienceRepository experienceRepository, IWorkTaskRepository workTaskRepository)
        {
            _experienceRepository = experienceRepository;
            _workTaskRepository = workTaskRepository;
        }

        public async Task<List<DashboardExpTaskResponse>> GetOpenTasksForUserAsync(int userId)
        {
            List<Experience> experiences = await _experienceRepository.GetCurrentExperiencseByUserIdAsync(userId);
            if (experiences.Count == 0)
            {
                //This section implies the user to be working somewhere, or more technically that they need to have an experience before having tasks.
                //remove this section when that is no longer the case.
                return [];
            }

            List<int> experienceIds = experiences.Select(exp => exp.ExperienceID).ToList();
            List<WorkTask> tasks = await _workTaskRepository.GetTaskByExperienceIdsAsync(experienceIds);
            ILookup<int, WorkTask> lookupTaskByExpID = tasks.ToLookup(task => task.ExperienceID);

            return experiences.Select(exp =>
            {
                ILookup<int?, WorkTask> parentLookup = lookupTaskByExpID[exp.ExperienceID].ToLookup(task => task.ParentTaskID);
                List<WorkTask> openTasks = parentLookup[null].Where(task => !task.IsDone).OrderBy(task => task.CreatedDate).ToList();
                int taskCounter = 0;

                return new DashboardExpTaskResponse
                {
                    ExperienceID = exp.ExperienceID,
                    CompanyName = exp.CompanyName,
                    RoleTitle = exp.RoleTitle,
                    OpenTaskCount = openTasks.Count,
                    Tasks = openTasks.TakeWhile(task => taskCounter < MaxTasksPerExperienceForDashboard).Select(task => 
                    {
                        taskCounter++;
                        return new DashboardTaskMiniResponse
                        {
                            WorkTaskID = task.WorkTaskID,
                            Description = task.Description,
                            IsDone = task.IsDone,
                            SubTasks = parentLookup[task.WorkTaskID].OrderBy(sub => sub.CreatedDate).TakeWhile(sub => taskCounter < MaxTasksPerExperienceForDashboard).Select(sub =>
                            {
                                taskCounter++;
                                return new DashboardSubTaskResponse
                                {
                                    WorkTaskID = sub.WorkTaskID,
                                    Description = sub.Description,
                                    IsDone = task.IsDone
                                };
                            }).ToList()
                        };
                    }).ToList()
                };
            }).Where(group => group.OpenTaskCount > 0).ToList();
        }
    }
}
