using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zer.Services.Logs.Dto;

namespace Zer.Services
{
    public class TempDataService:ITempDataService
    {
        public List<LogsDto> GetLogsByDate(DateTime starTime, DateTime endTime)
        {
            return new List<LogsDto>();
            //return new List<LogsDto>()
            //{
            //    new LogsDto(){Id = 1,ActionType = ActionType.Add,ActionModel = "用户管理",Content = "sss~jj~Jj~jj~Jj",CreateTime = DateTime.Now,IP = "192.168.1.1",MAC = "00-23-5A-15-99-42",UserId = 0001,UserName = "张三"},
            //    new LogsDto(){Id = 2,ActionType = ActionType.Add,ActionModel = "用户管理",Content = "sss~jj~Jj~jj~Jj",CreateTime = DateTime.Now,IP = "192.168.1.1",MAC = "00-23-5A-15-99-42",UserId = 0001,UserName = "张三"},
            //    new LogsDto(){Id = 3,ActionType = ActionType.Add,ActionModel = "用户管理",Content = "sss~jj~Jj~jj~Jj",CreateTime = DateTime.Now,IP = "192.168.1.1",MAC = "00-23-5A-15-99-42",UserId = 0001,UserName = "张三"},
            //    new LogsDto(){Id = 4,ActionType = ActionType.Add,ActionModel = "用户管理",Content = "sss~jj~Jj~jj~Jj",CreateTime = DateTime.Now,IP = "192.168.1.1",MAC = "00-23-5A-15-99-42",UserId = 0001,UserName = "张三"},
            //};
        }
    }
}
