namespace SezApi.Model.Request
{
    public class RequestFumigationCharges
    {
        public int FumigationChargeId { get; set; }
        public string ChargesFor { get; set; }
        public int ContainerSize { get; set; } 
        public decimal? FromWeight { get; set; } 
        public decimal? ToWeight { get; set; } 
        public decimal? Rate { get; set; } 
    }
}
