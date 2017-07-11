using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Entities.User
{
    public class UserInfo
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public UserState State { get; set; }
    }

    public enum UserState
    {
        Active = 0,
        Frozen = 1
    }
}
