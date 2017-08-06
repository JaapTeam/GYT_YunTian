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
                cfg.CreateMap<LogInfo, LogInfoDto>().ReverseMap();
//                cfg.CreateMap<UserInfo, UserInfoDto>()
//                    .ForMember(dest => dest.UserId, opt => opt.MapFrom(u => u.Id))
//                    .ReverseMap();

                cfg.CreateMap<UserInfo, UserInfoDto>().ForMember(dest => dest.UserId, opt => opt.MapFrom(u => u.Id));
                cfg.CreateMap<UserInfoDto, UserInfo>().ForMember(dest => dest.Id, opt => opt.MapFrom(u => u.UserId));
                
                cfg.CreateMap<LngAllowanceInfo, LngAllowanceInfoDto>().ReverseMap();            });

            _hasInitialzed = true;
        }
    }
}
