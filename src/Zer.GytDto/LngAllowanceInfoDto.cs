namespace Zer.GytDto
{
    public class LngAllowanceInfoDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public int LotId { get; set; }

        public string TruckNo { get; set; }
        /// <summary>
        /// 发动机号
        /// </summary>
        public string EngineId { get; set; }

        /// <summary>
        /// 气缸1 Id
        /// </summary>
        public string CylinderDefaultId { get; set; }
        /// <summary>
        /// 气缸2 Id
        /// </summary>
        public string CylinderSeconedId { get; set; }
    }
}