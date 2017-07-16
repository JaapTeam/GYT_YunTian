using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zer.Framework.Attributes;
using Zer.Framework.Export.Attributes;

namespace Zer.Services.Company.Dto
{
    public class CompanyInfoDto
    {
        [Sort(1)]
        [ExportDisplayName("编号")]
        public int Id { get; set; }

        [Sort(2)]
        [ExportDisplayName("公司名称")]
        public string CompanyName { get; set; }

        [Sort(3)]
        [ExportDisplayName("经营范围")]
        public string TraderRange { get; set; }
    }
}
