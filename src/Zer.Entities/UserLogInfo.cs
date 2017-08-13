using System;
using System.ComponentModel.DataAnnotations;
using Zer.Framework.Entities;

namespace Zer.Entities
{
    [Serializable]
    public class UserLogInfo : EntityBase,ICreateTime
    {
        public ActionType ActionType { get; set; }

        [MaxLength(50)]
        public string ActionModel { get; set; }

        public string Content { get; set; }

        public DateTime CreateTime { get; set; }

        [MaxLength(50)]
        public string IP { get; set; }

        [MaxLength(50)]
        public string MAC { get; set; }
        public int UserId { get; set; }

        [MaxLength(50)]
        public string DisplayName { get; set; }
    }

    public enum ActionType
    {
        Add,
        Delete
    }
}