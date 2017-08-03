namespace Zer.Framework.Entities
{
    public class EntityBase:IEntity<int>,IRecordState
    {
        public int Id { get; set; }
        public State State { get; set; }
    }
}
