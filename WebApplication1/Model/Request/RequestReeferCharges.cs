namespace SezApi.Model.Request
{
    public class RequestReeferCharges
    {
        public int ReeferChrgId { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public decimal? ElectricityCharge { get; set; }
        public string SacCode { get; set; }
        public string ContainerSize { get; set; } 
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; } 
    }
}
