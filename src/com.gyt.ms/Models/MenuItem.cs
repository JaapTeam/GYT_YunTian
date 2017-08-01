using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.gyt.ms.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public bool IsCurrentPage { get; set; }
        public string TextInfo { get; set; }
        public string Icon { get; set; }
        public List<MenuItem> ChildItems { get; set; }
    }
}