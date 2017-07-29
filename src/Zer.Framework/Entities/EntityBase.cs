namespace Zer.Framework.Entities
{
    public abstract class EntityBase : IEntity<int>
    {
        protected EntityBase()
        {
            DeleteState = DeleteState.Active;
        }

        public int Id { get; set; }
        public DeleteState DeleteState { get; set; }
    }
}
