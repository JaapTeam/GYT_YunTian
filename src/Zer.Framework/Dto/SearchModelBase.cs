
using System;
using System.Configuration;
using Zer.Framework.Cache;

namespace Zer.Framework.Dto
{
    public abstract class SearchDtoBase : IPaging, ISearchFilter
    {
        private int _pageIndex;
        private int _pageSize;
        private int _total = 0;
        private int _pageCount = 0;

        protected SearchDtoBase()
        {
            _pageIndex = 1;

            var pageSize = CacheHelper.GetCache("PageSize");

            if (pageSize == null)
            {
                if (!int.TryParse(ConfigurationManager.AppSettings["PageSize"], out _pageSize))
                {
                    _pageSize = 20;
                }

                CacheHelper.SetCache("PageSize",_pageSize);
                return;
            }

            _pageSize = Convert.ToInt32(pageSize);
        }

        public int PageCount
        {
            get
            {
                if (_pageCount <= 0)
                {
                    _pageCount = _total % PageSize == 0
                        ? _total / PageSize
                        : 1 + (_total / PageSize);
                }
                return _pageCount;
            }
            set { _pageCount = value; }
        }

        public int Total
        {
            get { return _total; }
            set { _total = value; }
        }

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set { _pageSize = value; }
        }

        public int PageIndex
        {
            get
            {
                if (_pageIndex <= 0)
                {
                    _pageIndex = 1;
                } 
                return _pageIndex;
            }
            set { _pageIndex = value; }
        }
    }
}

