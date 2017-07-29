namespace Zer.Framework.Entities
{
    public interface IEntity<TPrimeryKey> : IDeletedState
    {
        TPrimeryKey Id { get; set; }
    }
}