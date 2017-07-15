using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zer.Services.Users.Dto
{
    public enum FrozenResult
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success=0,
        /// <summary>
        /// 已经是冻结状态
        /// </summary>
        UserIsFrzon=1,
    }
}
