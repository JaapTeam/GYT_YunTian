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
        public static T Map<T>(this object entity)
            where T : class ,new()
        {
            return Mapper.Map<T>(entity);
        }

        public static IEnumerable<T> Map<T>(this IEnumerable<object> entities)
        {
            return Mapper.Map<IEnumerable<T>>(entities.ToList());
        }
    }
}
