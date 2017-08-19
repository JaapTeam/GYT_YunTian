using System;
using Zer.Entities;
using Zer.Framework.Attributes;
using Zer.Framework.Dto;
using Zer.Framework.Export.Attributes;

namespace Zer.GytDto
{
    public class GYTInfoDto : DtoBase
    {
        [ExportSort(1)]
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
        [ExportSort(2)]
        [ImportSort(1)]
        [ExportDisplayName("业务类型")]
        public BusinessType BusinessType { get; set; }

        [ExportSort(3)]
        [ExportDisplayName("原企业")]
        [ImportSort(2)]
        public string OriginalCompanyName { get; set; }

        /// <summary>
        ///原车牌号
        /// </summary>
        [ExportSort(4)]
        [ExportDisplayName("原车牌号")]
        [ImportSort(3)]
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
        [ExportSort(5)]
        [ExportDisplayName("申办企业名称")]
        [ImportSort(4)]
        public string BidCompanyName { get; set; }

        /// <summary>
        /// 申报车牌号
        /// </summary>
        [ExportSort(6)]
        [ExportDisplayName("申办车牌号")]
        [ImportSort(5)]
        public string BidTruckNo { get; set; }

        /// <summary>
        /// 申办日期
        /// </summary>
        [ExportSort(7)]
        [ExportDisplayName("申办日期")]
        [ImportSort(6)]
        public DateTime BidDate { get; set; }

        /// <summary>
        /// 经办人
        /// </summary>
        [ExportSort(9)]
        [ExportDisplayName("经办人")]
        [ImportSort(8)]
        public string BidName { get; set; }

        [ImprotIgnore]
        [ExportIgnore]
        public string BidDisplayName { get; set; }

        /// <summary>
        /// 业务状态
        /// </summary>
        [ExportSort(10)]
        [ImprotIgnore]
        [ExportDisplayName("业务状态")]
        public BusinessState Status { get; set; }
    }

}
