using System;
using Zer.Framework.Entities;

namespace Zer.Entities
{
    public class LogInfo : EntityBase,ICreateTime
    {
        public ActionType ActionType { get; set; }
        public string ActionModel { get; set; }
        public string Content { get; set; }

        public DateTime CreateTime { get; set; }
        public string IP { get; set; }
        public string MAC { get; set; }
        public int UserId { get; set; }
        public string DisplayName { get; set; }
    }

    public enum ActionType
    {
        Add,
        Delete
    }
}