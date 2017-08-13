using System;
using Zer.Framework.Entities;

namespace Zer.Entities
{
    [Serializable]
    public class CompanyInfo : EntityBase
    {
        public string CompanyName { get; set; }

        /// <summary>
        /// 企业经营范围
        /// </summary>
        public string TraderRange { get; set; }
    }
}
