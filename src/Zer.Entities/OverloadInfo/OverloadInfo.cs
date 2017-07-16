﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zer.Entities.OverloadInfo
{
    public class OverloadInfo : EntityBase
    {
        public int CompanyId { get; set; }

        public string FrontTruckNo { get; set; }

        public string BehindTruckNo { get; set; }

        public int DriverId { get; set; }
        public DateTime OverloadDateTime { get; set; }

        public OverloadMatter OverloadMatter { get; set; }
        public decimal GrossWeight { get; set; }
        public int AxisNumber { get; set; }
        public string Source { get; set; }
    }

    public enum OverloadMatter
    {
        深圳超限
    }
}
