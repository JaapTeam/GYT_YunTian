namespace Zer.Framework.Entities
{
    public interface IEntity
    {
    }

    public interface IEntity<TPrimeryKey> :IEntity
    {
        TPrimeryKey Id { get; set; }
    }
}