using AutoMapper;
using Zer.Entities;
using Zer.Services.Company.Dto;

namespace com.gyt.ms
{
    internal static class AutoMapperConfig
    {
        public static void MapperRegist()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<CompanyInfo, CompanyInfoDto>().ReverseMap();
            });
        }
    }
}