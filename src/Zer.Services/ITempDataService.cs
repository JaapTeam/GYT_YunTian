using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zer.Services.Logs.Dto;

namespace Zer.Services
{
    public interface ITempDataService
    {
        List<LogsDto> GetLogsByDate(DateTime starTime,DateTime endTime);
    }
}
