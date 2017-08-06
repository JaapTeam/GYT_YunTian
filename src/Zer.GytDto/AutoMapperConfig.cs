using AutoMapper;
using Zer.Entities;
using Zer.GytDto.Users;

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
                cfg.CreateMap<UserInfo, UserInfoDto>().ReverseMap();

                cfg.CreateMap<LngAllowanceInfo, LngAllowanceInfoDto>().ReverseMap();
                cfg.CreateMap<TruckInfo, TruckInfoDto>().ReverseMap();
            });

            _hasInitialzed = true;
        }
    }
}
