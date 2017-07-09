using System.Collections.Generic;
using LearningForAbp.OverloadRecords.Dtos;

namespace LearningForAbp.OverloadRecords
{
    public interface IOverloadRecordsAppService
    {
        GetOverloadRecordsOutput GetAll();
        OverloadRecordDto GetById(SearchOverloadRecordInput input);
        void Add(AddOverloadRecordInput input);
    }
}