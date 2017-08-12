using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zer.Entities;
using Zer.Framework.Dto;

namespace Zer.GytDto.SearchFilters
{
    public class GYTInfoSearchDto:SearchDtoBase
    {
        public int CompanyId { get; set; }
        public string TruckNo { get; set; }

        public BusinessState Status { get; set; }
    }
}
