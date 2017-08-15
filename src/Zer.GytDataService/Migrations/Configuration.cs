using Zer.Entities;

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

        protected override void Seed(GytDbContext context)
        {
            context.UserInfos.AddOrUpdate(
                u=>u.UserName,
                // √‹¬Î:123456
                new UserInfo() { DisplayName = "π‹¿Ì‘±", UserName = "admin888", Password = "14e1b600b1fd579f47433b88e8d85291" }
                );
        }
    }
}
