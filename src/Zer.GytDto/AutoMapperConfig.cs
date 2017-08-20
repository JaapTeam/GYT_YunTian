using AutoMapper;
using Zer.Entities;
using Zer.GytDto.OutputDto;
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
                cfg.CreateMap<UserLogInfo, LogInfoDto>().ReverseMap();
//                cfg.CreateMap<UserInfo, UserInfoDto>()
//                    .ForMember(dest => dest.UserId, opt => opt.MapFrom(u => u.Id))
//                    .ReverseMap();

                cfg.CreateMap<UserInfo, UserInfoDto>().ForMember(dest => dest.UserId, opt => opt.MapFrom(u => u.Id));
                cfg.CreateMap<UserInfoDto, UserInfo>().ForMember(dest => dest.Id, opt => opt.MapFrom(u => u.UserId));
                cfg.CreateMap<LngAllowanceInfo, LngAllowanceInfoDto>().ReverseMap();
                cfg.CreateMap<TruckInfo, TruckInfoDto>().ReverseMap();
                cfg.CreateMap<PeccancyInfo, PeccancyRecrodDto>().ReverseMap();
                cfg.CreateMap<SystemLogInfo, SystemLogInfoDto>().ReverseMap();
                cfg.CreateMap<GYTInfo, GYTInfoDto>().ReverseMap();
                cfg.CreateMap<CompanyInfo, PeccancyGroupByCompanyDto>();
            });

            _hasInitialzed = true;
        }
    }
}
