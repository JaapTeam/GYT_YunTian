namespace Zer.Framework.Entities
{
    public interface IEntity<TPrimeryKey> 
    {
        TPrimeryKey Id { get; set; }
    }
}