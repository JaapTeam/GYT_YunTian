using Zer.Entities;
using Zer.Framework.Attributes;
using Zer.Framework.Dto;

namespace Zer.GytDto
{
    public class TruckInfoDto : DtoBase
    {
        public int Id { get; set; }
        /// <summary>
        /// 前车牌号
        /// </summary>
        public string FrontTruckNo { get; set; }
        /// <summary>
        /// 后车牌号
        /// </summary>
        public string BehindTruckNo { get; set; }
        /// <summary>
        /// 公司编号 
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// 公司名称 
        /// </summary>
        public string CompanyName { get; set; }
       /// <summary>
       /// 驾驶员编号 
       /// </summary>
        public string DriverId { get; set; }
        /// <summary>
        /// 驾驶员姓名
        /// </summary>
        public string DriverName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public TruckState State { get; set; }
    }
}
