using System;
using Zer.Framework.Dto;

namespace Zer.GytDto.SearchFilters
{
    public class PeccancyWithCompanySearchDto : SearchDtoBase
    {
        public PeccancyWithCompanySearchDto()
        {
            MinCount = 0;
        }

        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public int? MinCount { get; set; }
    }
}