using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor.Installer;
using Zer.Framework.Exception;
using Zer.Framework.Extensions;

namespace Zer.Framework.UUID
{
    public class UUIdManager
    {
        private readonly List<long> _idPartList;
        private int _maxId = 0;
        private int _minId = 0;

        public static UUIdManager Instance { get; private set; }

        static UUIdManager()
        {
            Instance = new UUIdManager();
        }

        public UUIdManager()
        {
            _idPartList = new List<long>();

            if (!int.TryParse(ConfigurationManager.AppSettings["MaxId"], out _maxId))
            {
                _maxId = 99999;
            }

            if (!int.TryParse(ConfigurationManager.AppSettings["MinId"], out _minId))
            {
                _minId = 10000;
            }

            InitialzeIdList();
        }

        public List<long> Queue(int requiredCount)
        {
            if (requiredCount > _idPartList.Count)
            {
                throw new CustomException("超出索引范围","需要生成的Id数量",requiredCount.ToString());
            }

            if (_idPartList.Count <= requiredCount)
            {
                InitialzeIdList();
            }

            var result = _idPartList.GetRange(0, requiredCount);
            _idPartList.RemoveRange(0,requiredCount);
            return result;
        }

        public long Queue()
        {
            if (_idPartList.IsNullOrEmpty())
            {
                InitialzeIdList();
            }

            var value = _idPartList[0];
            _idPartList.RemoveAt(0);
            return value;
        }

        private void InitialzeIdList()
        {
            _idPartList.Clear();

            for (int i = _minId; i < _maxId; i++)
            {
                _idPartList.Add(i);
            }
        }
    }
}
