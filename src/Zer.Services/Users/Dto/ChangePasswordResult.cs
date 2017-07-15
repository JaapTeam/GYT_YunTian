using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zer.Services.Users.Dto
{
    public enum ChangePasswordResult
    {
        Success=0,
        
        SameAsOldPassword=1
    }
}
