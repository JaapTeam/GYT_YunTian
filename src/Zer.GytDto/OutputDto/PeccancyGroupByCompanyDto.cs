using Zer.Framework.Attributes;
using Zer.Framework.Export.Attributes;

namespace Zer.GytDto.OutputDto
{
    public class PeccancyGroupByCompanyDto:CompanyInfoDto
    {
        [ExportSort(6)]
        [ExportDisplayName("总违章次数")]
        public int PeccancyRecordCount { get; set; }

        [ExportSort(5)]
        [ExportDisplayName("已整改次数")]
        public int CanceledCount { get; set; }

        [ExportSort(4)]
        [ExportDisplayName("未整改次数")]
        public int UnCanceledCount { get; set; }
    }
}
