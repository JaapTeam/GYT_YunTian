using Zer.Entities;
using Zer.Framework.Dto;

namespace Zer.GytDto.SearchFilters
{
    public class PeccancySearchDto : SearchDtoBase
    {
        public int CompanyId { get; set; }
        public string TruckNo { get; set; }

        public Status Status { get; set; }
    }
}
