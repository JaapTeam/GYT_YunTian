using System;
using Zer.Framework.Attributes;
using Zer.Framework.Entities;
using Zer.Framework.Export.Attributes;

namespace Zer.Entities
{
    public class Log : EntityBase
    {
        public int UserId { get; set; }

        public int ActionType { get; set; }

        public string ActionModel { get; set; }

        public DateTime CreateTime { get; set; }

        public string MAC { get; set; }

        public string IP { get; set; }

        public string Content { get; set; }
    }
}