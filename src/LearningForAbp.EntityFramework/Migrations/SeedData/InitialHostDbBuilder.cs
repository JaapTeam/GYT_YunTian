using LearningForAbp.EntityFramework;
using EntityFramework.DynamicFilters;

namespace LearningForAbp.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly LearningForAbpDbContext _context;

        public InitialHostDbBuilder(LearningForAbpDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
