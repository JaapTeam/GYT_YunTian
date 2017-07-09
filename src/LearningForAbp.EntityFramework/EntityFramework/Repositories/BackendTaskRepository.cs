using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.EntityFramework;
using LearningForAbp.IRepositories;
using LearningForAbp.Tasks;

namespace LearningForAbp.EntityFramework.Repositories
{
    public class BackendTaskRepository : LearningForAbpRepositoryBase<Task>, IBackendTaskRepository
    {
        public BackendTaskRepository(IDbContextProvider<LearningForAbpDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public List<Task> GetTaskByAssignedPersonId(long personId)
        {
            var query = GetAll();
            if (personId > 0)
            {
                query = query.Where(x => x.AssignedPersonId == personId);
            }
            return query.ToList();
        }
    }
}
