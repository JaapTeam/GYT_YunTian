using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace Zer.Framework.Entities
{
    public class EntityBase:EntityBase<int>,IRecordState
    {
        protected EntityBase()
        {
            State = State.Active;
        }
        public State State { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
    }

    public abstract class EntityBase<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        [Key]
        public abstract TPrimaryKey Id { get; set; }
    }
}
