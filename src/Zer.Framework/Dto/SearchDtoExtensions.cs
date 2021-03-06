﻿using System.Linq;
using Zer.Framework.Entities;

namespace Zer.Framework.Dto
{
    public static class SearchDtoExtensions
    {
        public static IQueryable<T> ToPageQuery<T>(this IQueryable<T> query, IPaging searchDto)
            where T:IEntity
        {
            int count = query.Count();
            searchDto.Total = count;
            searchDto.PageCount = count % searchDto.PageSize == 0
                ? count / searchDto.PageSize
                : 1 + count / searchDto.PageSize;

            query = query.Skip(searchDto.PageSize * (searchDto.PageIndex - 1))
                .Take(searchDto.PageSize);
            return query;
        }

        //public static IQueryable<T> ToPagedQueryable<T>(this IQueryable<T> query, IPaging pagedDto)
        //    where T : EntityBase
        //{
        //    pagedDto.Total = query.Count();

        //    if (pagedDto.PageSize == 0)
        //    {
        //        pagedDto.PageSize = 20;
        //    }

        //    if (pagedDto.PageIndex == 0)
        //    {
        //        pagedDto.PageIndex = 1;
        //    }

        //    if (pagedDto.PageIndex > pagedDto.PageCount)
        //    {
        //        pagedDto.PageIndex = pagedDto.PageCount;
        //    }

        //    pagedDto.PageCount = pagedDto.Total % pagedDto.PageSize == 0
        //        ? pagedDto.Total / pagedDto.PageSize
        //        : 1 + (pagedDto.Total / pagedDto.PageSize);

        //    return query
        //        .Skip((pagedDto.PageIndex - 1) * pagedDto.PageSize)
        //        .Take(pagedDto.PageIndex * pagedDto.PageSize);
        //}
    }
}