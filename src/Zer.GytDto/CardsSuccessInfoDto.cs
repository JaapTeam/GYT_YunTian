using System;
using Zer.Framework.Attributes;
using Zer.Framework.Export.Attributes;

namespace Zer.GytDto
{
    public class CardsSuccessInfoDto : IValidateInputParameter
    {
        [Sort(1)]
        [ExportDisplayName("办理编号")]
        public string Id { get; set; }

        /// <summary>
        /// 原企业编号
        /// </summary>
        [ExportIgnore]
        public int OriginalCompanyId { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        [Sort(2)]
        [ExportDisplayName("业务类型")]
        public int BusinessType { get; set; }

        [Sort(3)]
        [ExportDisplayName("原企业")]
        public int OriginalCompanyName { get; set; }

        /// <summary>
        ///原车牌号
        /// </summary>
        [Sort(4)]
        [ExportDisplayName("原车牌号")]
        public string OriginalTruckNo { get; set; }

        /// <summary>
        /// 申办企业编号
        /// </summary>
        [ExportIgnore]
        public int RequestCompanyId { get; set; }

        /// <summary>
        /// 申办企业名称
        /// </summary>
        [Sort(5)]
        [ExportDisplayName("申办企业名称")]
        public int RequestCompanyName { get; set; }

        /// <summary>
        /// 申报车牌号
        /// </summary>
        [Sort(6)]
        [ExportDisplayName("申报车牌号")]
        public string RequestTruckNo { get; set; }

        /// <summary>
        /// 申办日期
        /// </summary>
        [Sort(7)]
        [ExportDisplayName("申办日期")]
        public DateTime RequestDate { get; set; }

        /// <summary>
        /// 经办人
        /// </summary>
        [Sort(8)]
        [ExportDisplayName("经办人")]
        public string TraderName { get; set; }

        [Sort(9)]
        [ExportDisplayName("申办结果")]
        public RequestResult RequestResult { get; set; }
    }

    /// <summary>
    /// 申办结果
    /// </summary>
    public enum RequestResult
    {
        Approved = 0,
        Reject = 1 
    }
}
