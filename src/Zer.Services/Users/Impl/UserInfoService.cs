using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zer.Services.Users.Dto;

namespace Zer.Services.Users.Impl
{
    public class UserInfoService : DomainServiceBase<UserInfoDto>
    {
        public override UserInfoDto GetById(int id)
        {
            throw new NotImplementedException();
        }

        public override List<UserInfoDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public override bool Add(UserInfoDto model)
        {
            throw new NotImplementedException();
        }

        public override bool AddRange(List<UserInfoDto> list)
        {
            throw new NotImplementedException();
        }
    }
}
