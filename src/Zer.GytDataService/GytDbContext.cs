using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using Zer.Entities;
using Zer.Framework.Entities;
using Zer.Framework.Exception;

namespace Zer.GytDataService
{
    public class GytDbContext : DbContext
    {
        public GytDbContext()
            :base("GytDbContext")
        {
            //Database.SetInitializer<GytDbContext>(null);
            //Configuration.AutoDetectChangesEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            int result;

            using (var transacton = this.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    result = base.SaveChanges();
                    transacton.Commit();
                }
                catch (DbEntityValidationException ex)
                {
                    transacton.Rollback();

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("<p>存在一个或多个以下错误，请校验数据后重试:</p>");
                    foreach (var dbValidationError in ex.EntityValidationErrors.SelectMany(x=>x.ValidationErrors))
                    {
                        var msg = $"<p>字段{dbValidationError.PropertyName}的值不符合规范,{dbValidationError.ErrorMessage}</p>";
                        sb.AppendLine(msg);
                    }

                    throw new CustomException(sb.ToString());
                }
                catch 
                {
                    transacton.Rollback();
                    throw;
                }
            }
            return result;
        }

        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<TruckInfo> TruckInfos { get; set; }
        public DbSet<PeccancyInfo> OverloadInfos { get; set; }
        public DbSet<CompanyInfo> CompanyInfos { get; set; }
        public DbSet<UserLogInfo> Logs { get; set; }
        public DbSet<LngAllowanceInfo> LngAllowanceInfos { get; set; }
        public DbSet<SystemLogInfo> SystemLogInfos { get; set; }
        public DbSet<GYTInfo> GytInfos { get; set; }
    }
}
