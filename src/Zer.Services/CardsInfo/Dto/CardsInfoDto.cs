using System;
using Zer.Framework.Attributes;
using Zer.Framework.Export.Attributes;

namespace Zer.Services.CardsInfo.Dto
{
    public class CardsInfoDto
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
        public int RequestCompanyId { get; set; }

        /// <summary>
        /// 申办企业名称
        /// </summary>
        [Sort(5)]
        [ExportDisplayName("申办企业名称")]
        public int RequestCompanyNamed { get; set; }

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
        public DateTime RequestDateTime { get; set; }

        /// <summary>
        /// 经办人
        /// </summary>
        [Sort(8)]
        [ExportDisplayName("经办人")]
        public string Handler { get; set; }

        /// <summary>
        /// 业务状态
        /// </summary>
        [Sort(9)]
        [ExportDisplayName("业务状态")]
        public BusinessState BusinessState { get; set; }

        /// <summary>
        /// 初审人编号
        /// </summary>
        [Ignore]
        public int FristAuditorId { get; set; }

        /// <summary>
        /// 再审人编号
        /// </summary>
        [Ignore]
        public int AgainAuditorId { get; set; }

        /// <summary>
        /// 终审人编号
        /// </summary>
        [Ignore]
        public int LastAuditorId { get; set; }
    }


    /// <summary>
    /// 业务类型
    /// </summary>
    public enum BusinessType
    {
        Gas = 0,
        Transfer = 1,
        New = 2
    }

    /// <summary>
    /// 信息状态
    /// </summary>
    public enum BusinessState
    {
        Initial = 0,
        FirstAuditor = 1,
        SingleAuditor = 2,
        HasBeenAuditor = 2
    }
}
