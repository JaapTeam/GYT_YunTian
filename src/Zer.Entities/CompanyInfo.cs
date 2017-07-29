using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zer.Framework.Entities;

namespace Zer.Entities
{
    public class CompanyInfo : EntityBase
    {
        public string CompanyName { get; set; }

        /// <summary>
        /// 企业经营范围
        /// </summary>
        public string TraderRange { get; set; }
    }
}
