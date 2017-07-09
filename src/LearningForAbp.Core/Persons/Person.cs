using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace LearningForAbp.Persons
{
    public class Person : Entity
    {
        public virtual  string Name { get; set; }
    }
}
