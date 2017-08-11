namespace Zer.Framework.Dto
{
    public interface IPaging
    {
        int PageIndex { get; set; }
        int PageSize { get; set; }
        int Total { get; set; }
    }

    public interface IPaging<T> : IPaging
    {
        
    }
}