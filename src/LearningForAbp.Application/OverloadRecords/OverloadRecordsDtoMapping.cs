using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LearningForAbp.OverloadRecords.Dtos;

namespace LearningForAbp.OverloadRecords
{
    public class OverloadRecordsDtoMapping:IDtoMapping
    {
        public void CreateMapping(IMapperConfigurationExpression mapperConfig)
        {
            mapperConfig.CreateMap<OverloadRecordDto, OverloadRecords>();
        }
    }
}
