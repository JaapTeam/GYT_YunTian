namespace Zer.Framework.Dto
{
    public interface IPaging
    {
        int PageIndex { get; set; }
        int PageSize { get; set; }
        int Total { get; set; }
        int PageCount { get; set; }
    }
}