using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zer.Framework;
using Zer.Services.LngAllowance.Dto;

namespace Zer.Services.LngAllowance
{
    public interface ILngAllowanceService : IAppService
    {
        List<LngAllowanceInfoDto> GetList(FilterMatchInputDto filterMatchInput);

        List<LngAllowanceInfoDto> GetList(int[] idList);

        LngAllowanceInfoDto GetById(int id);

        List<LngAllowanceInfoDto> GetAll();

        bool Add(LngAllowanceInfoDto model);

        bool AddRange(List<LngAllowanceInfoDto> list);
    }
}
