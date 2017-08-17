using Zer.Framework.Attributes;
using Zer.Framework.Dto;
using Zer.Framework.Export.Attributes;

namespace Zer.GytDto
{
    public class CompanyInfoDto : DtoBase
    {
        [ExportSort(1)]
        [ExportDisplayName("编号")]
        public int Id { get; set; }

        [ExportSort(2)]
        [ExportDisplayName("公司名称")]
        public string CompanyName { get; set; }

        [ExportSort(3)]
        [ExportDisplayName("经营范围")]
        public string TraderRange { get; set; }
    }
}
