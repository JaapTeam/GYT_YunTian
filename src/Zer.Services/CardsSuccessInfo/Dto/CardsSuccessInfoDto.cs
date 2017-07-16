using System;
using Zer.Framework.Attributes;
using Zer.Framework.Export.Attributes;
using Zer.Services.CardsInfo.Dto;

namespace Zer.Services.CardsSuccessInfo.Dto
{
    public class CardsSuccessInfoDto
    {
        [Sort(1)]
        [ExportDisplayName("办理编号")]
        public string Id { get; set; }

        /// <summary>
        /// 原企业编号
        /// </summary>
        [Ignore]
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
        [Ignore]
        public int BidCompanyId { get; set; }

        /// <summary>
        /// 申办企业名称
        /// </summary>
        [Sort(5)]
        [ExportDisplayName("申办企业名称")]
        public int BidCompanyNamed { get; set; }

        /// <summary>
        /// 申报车牌号
        /// </summary>
        [Sort(6)]
        [ExportDisplayName("申报车牌号")]
        public string BidTruckNo { get; set; }

        /// <summary>
        /// 申办日期
        /// </summary>
        [Sort(7)]
        [ExportDisplayName("申办日期")]
        public DateTime BidDateTime { get; set; }

        /// <summary>
        /// 经办人
        /// </summary>
        [Sort(8)]
        [ExportDisplayName("经办人")]
        public string Handler { get; set; }

        [Sort(9)]
        [ExportDisplayName("申办结果")]
        public BidResult BidResult { get; set; }
    }

    /// <summary>
    /// 申办结果
    /// </summary>
    public enum BidResult
    {
        Success = 0,
        Fail = 1 
    }
}
