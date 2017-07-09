using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;

namespace LearningForAbp.IRepositories
{
    public interface IOverloadRecordsRepository : IRepository<OverloadRecords.OverloadRecords>
    {
        void AddRang(List<OverloadRecords.OverloadRecords> list);
        OverloadRecords.OverloadRecords GetById(int id);
        void Add(OverloadRecords.OverloadRecords model);
    }
}
