using System;
using Zer.Entities;
using Zer.Framework.Attributes;
using Zer.Framework.Export.Attributes;

namespace Zer.GytDto
{
    public class GYTInfoDto
    {
        [Sort(1)]
        [ImprotIgnore]
        [ExportDisplayName("办理编号")]
        public string Id { get; set; }

        /// <summary>
        /// 原企业编号
        /// </summary>
        [ExportIgnore]
        [ImprotIgnore]
        public int OriginalCompanyId { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        [Sort(2)]
        [ExportDisplayName("业务类型")]
        public BusinessType BusinessType { get; set; }

        [Sort(3)]
        [ExportDisplayName("原企业")]
        public string OriginalCompanyName { get; set; }

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
        [ImprotIgnore]
        public int BidCompanyId { get; set; }

        /// <summary>
        /// 申办企业名称
        /// </summary>
        [Sort(5)]
        [ExportDisplayName("申办企业名称")]
        public string BidCompanyName { get; set; }

        /// <summary>
        /// 申报车牌号
        /// </summary>
        [Sort(6)]
        [ExportDisplayName("申办车牌号")]
        public string BidTruckNo { get; set; }

        /// <summary>
        /// 申办日期
        /// </summary>
        [Sort(7)]
        [ExportDisplayName("申办日期")]
        public DateTime BidDate { get; set; }
        /// <summary>
        /// 旧车牌号
        /// </summary>
        [Sort(8)]
        [ExportDisplayName("旧车牌号")]
        public string OldTruckNo { get; set; }

        /// <summary>
        /// 经办人
        /// </summary>
        [Sort(9)]
        [ExportDisplayName("经办人")]
        public string BidName { get; set; }

        /// <summary>
        /// 业务状态
        /// </summary>
        [Sort(10)]
        [ImprotIgnore]
        [ExportDisplayName("业务状态")]
        public BusinessState Status { get; set; }
    }

}
