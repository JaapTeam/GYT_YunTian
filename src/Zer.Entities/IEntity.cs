namespace Zer.Entities
{
    public interface IEntity<TPrimeryKey> 
    {
        TPrimeryKey Id { get; set; }
    }
}