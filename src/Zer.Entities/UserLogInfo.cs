using System;
using System.ComponentModel.DataAnnotations;
using Zer.Framework.Entities;

namespace Zer.Entities
{
    [Serializable]
    public class UserLogInfo : EntityBase,ICreateTime
    {
        public ActionType ActionType { get; set; }

        [StringLength(50)]
        public string ActionModel { get; set; }
        [MaxLength]
        public string Content { get; set; }

        public DateTime CreateTime { get; set; }

        [StringLength(20)]
        public string IP { get; set; }

        public int UserId { get; set; }

        [StringLength(20)]
        public string DisplayName { get; set; }
    }
}