namespace SezApi.Model.Request
{
    public class RequestRTChargesDtl
    {
        public int RTChargesDtlID { get; set; }
        public int RTChargesID { get; set; }
        public int WtSlabId { get; set; } 
        public int FromWtSlabCharge { get; set; } 
        public int ToWtSlabCharge { get; set; }
        public decimal? RateCWC { get; set; } 
        public int WeightSlab { get; set; } 
        public string? PortName { get; set;}
        public int PortId { get; set; } 
    }
}
