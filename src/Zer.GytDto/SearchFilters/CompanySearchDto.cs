using System.Collections.Generic;
using Zer.Framework.Dto;

namespace Zer.GytDto.SearchFilters
{
    public class CompanySearchDto:SearchDtoBase
    {
        public int? PeccancyRecoredCount { get; set; }
        public List<int> CompanyIdList { get; set; }
    }
}