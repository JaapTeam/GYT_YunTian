using System;
using System.Linq;
using System.Linq.Expressions;

namespace Zer.Data.Extensions
{
    public static class QueryableExpressions
    {
        /// <summary>
        /// 筛选数据
        /// </summary>
        /// <typeparam name="T">获取实体类型</typeparam>
        /// <param name="source">原表达式</param>
        /// <param name="expression">条件表达式</param>
        /// <returns></returns>
        public static IQueryable<T> Get<T>(this IQueryable<T> source, Expression<Func<T, bool>> expression)
            where T : class
        {
            return source.Where(expression);
        }
    }
}
