using System;
using System.Collections.Generic;
using System.Linq;
using Zer.Entities;

namespace Zer.GytDataService.Impl
{
    public class LogInfoDataService : GytRepository<LogInfo>, ILogInfoDataService
    {
        public override IQueryable<LogInfo> GetAll()
        {
            return new List<LogInfo>()
            {
                new LogInfo()
                {
                    Id = 1,
                    ActionType = ActionType.Add,
                    ActionModel = "用户管理",
                    Content = "sss~jj~Jj~jj~Jj",
                    CreateTime = DateTime.Now,
                    IP = "192.168.1.1",
                    MAC = "00-23-5A-15-99-42",
                    UserId = 0001,
                    DisplayName = "张三"
                },
                new LogInfo()
                {
                    Id = 2,
                    ActionType = ActionType.Add,
                    ActionModel = "用户管理",
                    Content = "sss~jj~Jj~jj~Jj",
                    CreateTime = DateTime.Now,
                    IP = "192.168.1.1",
                    MAC = "00-23-5A-15-99-42",
                    UserId = 0001,
                    DisplayName = "张三"
                },
                new LogInfo()
                {
                    Id = 3,
                    ActionType = ActionType.Add,
                    ActionModel = "用户管理",
                    Content = "sss~jj~Jj~jj~Jj",
                    CreateTime = DateTime.Now,
                    IP = "192.168.1.1",
                    MAC = "00-23-5A-15-99-42",
                    UserId = 0001,
                    DisplayName = "张三"
                },
                new LogInfo()
                {
                    Id = 4,
                    ActionType = ActionType.Add,
                    ActionModel = "用户管理",
                    Content = "sss~jj~Jj~jj~Jj",
                    CreateTime = DateTime.Now,
                    IP = "192.168.1.1",
                    MAC = "00-23-5A-15-99-42",
                    UserId = 0001,
                    DisplayName = "张三"
                },
            }.AsQueryable();
        }
    }
}