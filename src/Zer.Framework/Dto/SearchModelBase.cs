namespace Zer.Framework.Dto
{
    public abstract class SearchDtoBase : IPaging, ISearchFilter
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
    }
}
