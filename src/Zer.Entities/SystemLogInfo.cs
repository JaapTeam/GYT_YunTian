using System;
using System.ComponentModel.DataAnnotations;
using Zer.Framework.Entities;

namespace Zer.Entities
{
    [Serializable]
    public class SystemLogInfo : EntityBase
    {
        public int UserId { get; set; }
        [MaxLength]
        public string Content { get; set; }
        [StringLength(20)]
        public string ActionName { get; set; }
        [StringLength(20)]
        public string ControllerName { get; set; }
    }
}