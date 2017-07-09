using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LearningForAbp.IRepositories;
using LearningForAbp.OverloadRecords.Dtos;

namespace LearningForAbp.OverloadRecords
{
    public class OverloadRecordsAppService : LearningForAbpAppServiceBase,IOverloadRecordsAppService
    {
        private readonly IOverloadRecordsRepository _overloadRecordsRepository;
        public OverloadRecordsAppService(IOverloadRecordsRepository overloadRecordsRepository)
        {
            _overloadRecordsRepository = overloadRecordsRepository;
        }

        public GetOverloadRecordsOutput GetAll()
        {
            var query = _overloadRecordsRepository.GetAll();

            return new GetOverloadRecordsOutput
            {
                OverloadRecordDtos = Mapper.Map<List<OverloadRecordDto>>(query.ToList())
            };
        }

        public OverloadRecordDto GetById(SearchOverloadRecordInput input)
        {
            var query = _overloadRecordsRepository.GetAll().Where(x => x.Id == input.Id);
            return Mapper.Map<OverloadRecordDto>(query.FirstOrDefault());
        }

        public void Add(AddOverloadRecordInput input)
        {
            _overloadRecordsRepository.Add(Mapper.Map<OverloadRecords>(input));
        }
    }
}
