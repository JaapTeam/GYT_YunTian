using Zer.Entities;

namespace com.gyt.ms.Models
{
    public class HandleDataDto 
    {
        public string BidCompanyName { get; set; }
        public string BidTruckNo { get; set; }
        public string OldTruckNo { get; set; }
        public bool IsAnnual { get; set; }
        public bool? IsTransferRecrod { get; set; }
        public bool? IsGytStatus { get; set; }
        public bool? IsGytCancel { get; set; }
        public bool IsPeccancy { get; set; }
        public bool IsConsistentInfo { get; set; }
        public BusinessType BusinessType { get; set; }
        public bool Result { get; set; }
        public bool TargetIsUse { get; set; }
    }
}
