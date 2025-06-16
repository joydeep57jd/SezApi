namespace SezApi.Model.Request
{
    public class RequestHandlingCharges
    {
        public int? HandlingChargesID { get; set; }  
        public DateTime? EffectiveDate { get; set; } 
        public int? SacCodeId { get; set; }          
        public decimal? Rate { get; set; }           
        public decimal? MinRateperSBBOE { get; set; } 
        public int CreatedBy { get; set; }         
        public int? UpdatedBy { get; set; }         
    }
}
