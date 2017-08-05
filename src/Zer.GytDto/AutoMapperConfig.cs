using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Zer.Entities;
using Zer.GytDto.Users;

namespace Zer.GytDto
{
    public static class AutoMapperConfig
    {
        private static bool HasInitialzed = false;

        public static void Initialze()
        {
            if (HasInitialzed) return;

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<CompanyInfo, CompanyInfoDto>().ReverseMap();
                cfg.CreateMap<LogInfo, LogsDto>().ReverseMap();
                cfg.CreateMap<UserInfo, UserInfoDto>().ReverseMap();
                
            });

            HasInitialzed = true;
        }
    }
}
