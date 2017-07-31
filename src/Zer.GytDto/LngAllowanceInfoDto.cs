namespace Zer.GytDto
{
    public class LngAllowanceInfoDto
    {
        [Sort(1)]
        [ExportDisplayName("���")]
        public int Id { get; set; }

        [Sort(2)]
        [ExportDisplayName("��˾����")]
        public string CompanyName { get; set; }

        [Sort(3)]
        [ExportDisplayName("����")]
        public int LotId { get; set; }

        [Sort(4)]
        [ExportDisplayName("���ƺ�")]
        public string TruckNo { get; set; }

        [Sort(5)]
        [ExportDisplayName("��������")]
        public string EngineId { get; set; }

        [Sort(6)]
        [ExportDisplayName("����1���")]
        public string CylinderDefaultId { get; set; }

        [Sort(7)]
        [ExportDisplayName("����2���")]
        public string CylinderSeconedId { get; set; }
    }
}