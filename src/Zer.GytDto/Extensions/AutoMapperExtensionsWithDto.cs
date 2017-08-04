using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Zer.Framework.Entities;

namespace Zer.GytDto.Extensions
{
    public static class AutoMapperExtensionsWithDto
    {
        public static TDto Map<TDto>(this IEntity entity)
            where TDto : class ,new()
        {
            return Mapper.Map<TDto>(entity);
        }

        public static IEnumerable<TDto> Map<TDto>(this IEnumerable<IEntity> entities)
        {
            return Mapper.Map<IEnumerable<TDto>>(entities.ToList());
        }

        public static List<TDto> Map<TDto>(this List<IEntity> entities)
        {
            return Mapper.Map<List<TDto>>(entities.ToList());
        }
    }
}
