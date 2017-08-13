namespace Zer.Framework.Dto
{
    public abstract class SearchDtoBase : IPaging, ISearchFilter
    {
        private int _pageIndex = 1;
        private int _pageSize = 20;
        private int _total = 0;
        private int _pageCount = 0;

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
                if (_pageSize <= 0)
                {
                    _pageSize = 20;
                }

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

