using System;
using System.Collections.Generic;
using System.Linq;
using Zer.Entities;
using Zer.Framework.EntityFramework;

namespace Zer.GytDataService.Impl
{
    public class LogDataService : EFRepositoryBase<Logs, int>, ILogDataService
    {
        public LogDataService() : base(new GytDbContext())
        {
        }

        public override IQueryable<Logs> GetAll()
        {
            return new List<Logs>()
            {
                new Logs()
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
                new Logs()
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
                new Logs()
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
                new Logs()
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