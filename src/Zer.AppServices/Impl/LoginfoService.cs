using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Zer.AppServices.Extensions;
using Zer.Entities;
using Zer.Framework.Dto;
using Zer.GytDataService;
using Zer.GytDto;
using Zer.GytDto.SearchFilters;
using Zer.GytDto.Users;

namespace Zer.AppServices.Impl
{
    public class LogInfoService : ILogInfoService
    {
        private readonly ILogInfoDataService _logInfoDataService;

        public LogInfoService(ILogInfoDataService logInfoDataService)
        {
            _logInfoDataService = logInfoDataService;
        }

        public LogInfoDto GetById(int id)
        {
            return Mapper.Map<LogInfoDto>(_logInfoDataService.GetById(id));
        }

        public List<LogInfoDto> GetAll()
        {
            return new List<LogInfoDto>();
        }

        public List<LogInfoDto> GetAll(UserInfoDto userInfo)
        {
            var query = _logInfoDataService.GetAll().RoleFilter(userInfo);
            return Mapper.Map<List<LogInfoDto>>(query.ToList());
        }

        public List<LogInfoDto> GetList(LogInfoSearchDto filter, UserInfoDto userInfo)
        {
            var query = _logInfoDataService.GetAll().RoleFilter(userInfo).ToPageQuery(filter).ToList();

            return Mapper.Map<List<LogInfoDto>>(query);
        }



        public LogInfoDto Add(LogInfoDto model)
        {
            throw new System.NotImplementedException();
        }

        public List<LogInfoDto> AddRange(List<LogInfoDto> list)
        {
            throw new System.NotImplementedException();
        }

        public List<LogInfoDto> GetListByFilterMatch(FilterMatchInputDto filterMatch)
        {
            throw new System.NotImplementedException();
        }

        public List<LogInfoDto> GetListByIds(int[] ids, UserInfoDto userinfo)
        {
            return Mapper.Map<List<LogInfoDto>>(_logInfoDataService.GetAll().RoleFilter(userinfo).Where(x => ids.Contains(x.Id)).ToList());
        }

        public List<LogInfoDto> GetListByUserId(int userId,UserInfoDto userInfo)
        {
            return Mapper.Map<List<LogInfoDto>>(
                _logInfoDataService.GetAll()
                                   .RoleFilter(userInfo)
                                   .Where(x=>x.UserId==userId)
                                   .ToList());
        }
    }
}