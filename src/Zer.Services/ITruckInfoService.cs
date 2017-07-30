using System;
using System.Collections.Generic;
using Zer.Framework;
using Zer.GytDto;

namespace Zer.Services
{
    public interface ITruckInfoService :IAppService
    {
        
        TruckInfoDto GetByTruckNo(string truckNo);

        List<TruckInfoDto> GetListByCompanyId(int id);

        bool TruckNoExists(string truckNo);

        TruckInfoDto GetById(int id);

        List<TruckInfoDto> GetAll();

        bool Add(TruckInfoDto model);

        bool AddRange(List<TruckInfoDto> list);
    }

    public class TruckInfoService : ITruckInfoService
    {
        public TruckInfoDto GetByTruckNo(string truckNo)
        {
            throw new NotImplementedException();
        }

        public List<TruckInfoDto> GetListByCompanyId(int id)
        {
            throw new NotImplementedException();
        }

        public bool TruckNoExists(string truckNo)
        {
            throw new NotImplementedException();
        }

        public TruckInfoDto GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<TruckInfoDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Add(TruckInfoDto model)
        {
            throw new NotImplementedException();
        }

        public bool AddRange(List<TruckInfoDto> list)
        {
            throw new NotImplementedException();
        }
    }
}
