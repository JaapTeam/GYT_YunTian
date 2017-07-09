using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using Castle.MicroKernel.Registration;

namespace LearningForAbp
{
    [DependsOn(typeof(LearningForAbpCoreModule), typeof(AbpAutoMapperModule))]
    public class LearningForAbpApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(mapper =>
            {
                // Add your custom AutoMapper mappings here...
                // mapper.CreateMap<,>()
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            // 注册IDtoMapping
            IocManager.IocContainer.Register(
                Classes.FromAssembly(Assembly.GetExecutingAssembly())
                .IncludeNonPublicTypes()
                .BasedOn<IDtoMapping>()
                .WithServiceSelf()
                .WithServiceDefaultInterfaces()
                .LifestyleTransient());

            // 解析依赖，并进行映射规则创建

           
            Configuration.Modules.AbpAutoMapper().Configurators.Add(mapper =>
            {
                var mappers = IocManager.IocContainer.ResolveAll<IDtoMapping>();
                foreach (var dtomap in mappers)
                {
                    dtomap.CreateMapping(mapper);
                }
            });
        }
    }
}
