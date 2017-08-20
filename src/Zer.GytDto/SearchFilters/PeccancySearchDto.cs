using System;
using Zer.Entities;
using Zer.Framework.Dto;

namespace Zer.GytDto.SearchFilters
{
    public class PeccancySearchDto : SearchDtoBase
    {
        public string CompanyName { get; set; }
        public string TruckNo { get; set; }
        public Status? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
