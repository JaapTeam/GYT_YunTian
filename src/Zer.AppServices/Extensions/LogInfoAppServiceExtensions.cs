using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zer.Entities;
using Zer.GytDto.Users;

namespace Zer.AppServices.Extensions
{
    public static class LogInfoAppServiceExtensions
    {
        public static IQueryable<UserLogInfo> RoleFilter(this IQueryable<UserLogInfo> query, UserInfoDto userInfo)
        {
            if (userInfo.Role == RoleLevel.User)
            {
                query = query.Where(x => x.UserId == userInfo.UserId);
            }
            return query;
        }
    }
}
