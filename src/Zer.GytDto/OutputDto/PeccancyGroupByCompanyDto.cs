using Zer.Framework.Attributes;
using Zer.Framework.Export.Attributes;

namespace Zer.GytDto.OutputDto
{
    public class PeccancyGroupByCompanyDto:CompanyInfoDto
    {
        [ExportSort(4)]
        [ExportDisplayName("违章次数")]
        public int PeccancyRecordCount { get; set; }
    }
}
