using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Domain.Repositories;
using LearningForAbp.Tasks;

namespace LearningForAbp.IRepositories
{
    public interface IBackendTaskRepository: IRepository<Task>
    {
        List<Task> GetTaskByAssignedPersonId(long personId);
    }
}
