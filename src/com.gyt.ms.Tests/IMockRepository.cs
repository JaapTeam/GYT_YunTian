using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Zer.Services.Users;

namespace com.gyt.ms.Tests
{
    public interface IMockRepository
    {
        HttpContext GetMockHttpContext();
        IUserInfoService GetMockUserInfoService();
    }

    public class MockRepository : IMockRepository
    {
        public HttpContext GetMockHttpContext()
        {
            throw new NotImplementedException();
        }

        public IUserInfoService GetMockUserInfoService()
        {
            throw new NotImplementedException();
        }
    }
}
