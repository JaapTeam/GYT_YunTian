using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMS.Entities.User;

namespace Zer.Services.Users.Dto
{
    public class UserInfoDto
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public UserState State { get; set; }
    }
}
