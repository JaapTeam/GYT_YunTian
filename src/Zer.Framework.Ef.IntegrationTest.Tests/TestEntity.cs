using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zer.Framework.Entities;

namespace Zer.Framework.Ef.IntegrationTest.Tests
{
    public class TestEntity : EntityBase, ICreateTime
    {
        public string StringField { get; set; }
        public bool BoolenField { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
