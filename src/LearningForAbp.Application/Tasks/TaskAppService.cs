using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using AutoMapper;
using LearningForAbp.Tasks.Dtos;

namespace LearningForAbp.Tasks
{
    public class TaskAppService : LearningForAbpAppServiceBase, ITaskAppService
    {
        private readonly IRepository<Task> _taskRepository;

        public TaskAppService(IRepository<Task> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public GetTasksOutput GetTasks(GetTasksInput getTasksInput)
        {
            var query = _taskRepository.GetAll();

            query = getTasksInput.AssignedPersonId.HasValue
                ? query.Where(x => x.AssignedPersonId == getTasksInput.AssignedPersonId.Value)
                : query;

            query = getTasksInput.State.HasValue
                ? query.Where(x => x.State == getTasksInput.State.Value)
                : query;

            return new GetTasksOutput
            {
                Tasks =  Mapper.Map<List<TaskDto>>(query.ToList())
            };
        }
    }
}
