namespace Zer.Framework.Entities
{
    public class EntityBase : IEntity<int>
    {
        public int Id { get; set; }
        public DeleteState DeleteState { get; set; }
    }
}
