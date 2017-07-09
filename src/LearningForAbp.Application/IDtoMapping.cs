using AutoMapper;

namespace LearningForAbp
{
    public interface IDtoMapping
    {
        void CreateMapping(IMapperConfigurationExpression mapperConfig);
    }
}