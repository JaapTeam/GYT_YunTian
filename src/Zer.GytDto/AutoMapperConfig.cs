using AutoMapper;
using Zer.Entities;

namespace Zer.GytDto
{
    public static class AutoMapperConfig
    {
        private static bool _hasInitialzed;

        static AutoMapperConfig()
        {
            _hasInitialzed = false;
        }

        public static void Initialze()
        {
            if (_hasInitialzed) return;

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<CompanyInfo, CompanyInfoDto>().ReverseMap();
                cfg.CreateMap<LogInfo, LogsDto>().ReverseMap();
                cfg.CreateMap<LngAllowanceInfo, LngAllowanceInfoDto>().ReverseMap();
            });

            _hasInitialzed = true;
        }
    }
}
