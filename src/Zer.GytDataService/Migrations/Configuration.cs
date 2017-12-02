using Zer.Entities;
using Zer.Framework.Entities;

namespace Zer.GytDataService.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Zer.GytDataService.GytDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Zer.GytDataService.GytDbContext context)
        {
            context.UserInfos.AddOrUpdate(
                u => u.UserName,
                // ÃÜÂë:123456
                new UserInfo() { DisplayName = "¹ÜÀíÔ±", UserName = "admin888", Role = RoleLevel.Administrator, Password = "14e1b600b1fd579f47433b88e8d85291" }
            );

            context.CompanyInfos.AddOrUpdate(company => company.Id,
                new CompanyInfo() {CompanyName = "", State = State.Active, Id = 0, TraderRange = ""});
        }
    }
}
