using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zer.Services.LngAllowance.Dto;

namespace Zer.Services.LngAllowance
{
    public interface ILngAllowanceService:IDomainService<LngAllowanceInfoDto,int>
    {
        List<LngAllowanceInfoDto> GetList(FilterMatchInputDto filterMatchInput);

        List<LngAllowanceInfoDto> GetList(int[] idList);
    }
}
