using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zer.Entities
{
    public class EntityBase:IEntity<int>,IRecordState
    {
        public int Id { get; set; }
        public State State { get; set; }
    }
}
