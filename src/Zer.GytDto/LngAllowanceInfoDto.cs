namespace Zer.GytDto
{
    public class LngAllowanceInfoDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public int LotId { get; set; }

        public string TruckNo { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public string EngineId { get; set; }

        /// <summary>
        /// ����1 Id
        /// </summary>
        public string CylinderDefaultId { get; set; }
        /// <summary>
        /// ����2 Id
        /// </summary>
        public string CylinderSeconedId { get; set; }
    }
}