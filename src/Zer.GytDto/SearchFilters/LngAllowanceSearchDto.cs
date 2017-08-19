using Zer.Entities;
using Zer.Framework.Dto;

namespace Zer.GytDto.SearchFilters
{
    public class LngAllowanceSearchDto : SearchDtoBase
    {
        public string CompanyName { get; set; }
        public string TruckNo { get; set; }
        public LngStatus? IsAllowanced { get; set; }
    }
}