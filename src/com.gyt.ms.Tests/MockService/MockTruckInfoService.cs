using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zer.Services.Truck;
using Zer.Services.Truck.Dto;

namespace com.gyt.ms.Tests.MockService
{
    public class MockTruckInfoService : MockRepository<ITruckInfoService, TruckInfoDto>
    {
        protected override void SetMock()
        {
            Mock.Setup(x => x.GetByTruckNo("粤B12345"))
                .Returns(new TruckInfoDto());

            Mock.Setup(x => x.GetByTruckNo("粤B54321"))
                .Returns(new TruckInfoDto()
                {
                    Id = 1, ConpanyName = "天空物流", DriverName = "张三", FrontTruckNo = "粤B54321" 
                    
                });

            Mock.Setup(x => x.GetListByCompanyId(1))
                .Returns(new List<TruckInfoDto>());

            Mock.Setup(x => x.GetListByCompanyId(2))
                .Returns(new List<TruckInfoDto>()
                {
                    new TruckInfoDto(){Id = 1, ConpanyName = "天空物流", DriverName = "张三", FrontTruckNo = "粤B54321" },
                    new TruckInfoDto(){Id = 2, ConpanyName = "天空物流", DriverName = "李四", FrontTruckNo = "粤B12345" },
                });
        }
    }
}
