using System.Collections.Generic;
using Abp.AutoMapper;

namespace LearningForAbp.Tasks.Dtos
{
    [AutoMap(typeof(Task))]
    public class GetTasksOutput
    {
        public List<TaskDto> Tasks { get; set; }
    }
}