using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LearningForAbp.Tasks.Dtos;

namespace LearningForAbp.Tasks
{
    public class TaskDtoMapping : IDtoMapping
    {
        public void CreateMapping(IMapperConfigurationExpression mapperConfig)
        {
            mapperConfig.CreateMap<CreateTaskInput, Task>();
            mapperConfig.CreateMap<UpdateTaskInput, Task>();
            mapperConfig.CreateMap<TaskDto, UpdateTaskInput>();

            var taskDtoMapper = mapperConfig.CreateMap<Task, TaskDto>();
            taskDtoMapper.ForMember(
                dto => dto.AssignedPersonName,
                map => map.MapFrom(model => model.AssignedPerson.FullName));
        }
    }
}
