﻿using System.Collections.Generic;
using Zer.Framework;
using Zer.Services.OverloadRecrod.Dto;

namespace Zer.Services.OverloadRecrod
{
    public interface IOverloadRecrodService : IAppService
    {
        List<OverloadRecrodDto> GetListByFilterMatch(FilterMatchInputDto filterMatch);

        bool ChangedById(int id);

        List<OverloadRecrodDto> GetListByIds(int[] ids);

        List<OverloadRecrodDto> GetList(int[] idList);

        OverloadRecrodDto GetById(int id);

        List<OverloadRecrodDto> GetAll();

        bool Add(OverloadRecrodDto model);

        bool AddRange(List<OverloadRecrodDto> list);
    }
}
