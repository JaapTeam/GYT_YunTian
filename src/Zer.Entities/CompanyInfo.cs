using System;
using System.ComponentModel.DataAnnotations;
using Zer.Framework.Entities;

namespace Zer.Entities
{
    [Serializable]
    public class CompanyInfo : EntityBase
    {
        [StringLength(50)]
        public string CompanyName { get; set; }

        /// <summary>
        /// 企业经营范围
        /// </summary>
        [StringLength(200)]
        public string TraderRange { get; set; }
    }
}
