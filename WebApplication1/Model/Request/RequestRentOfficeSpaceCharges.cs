namespace SezApi.Model.Request
{
    public class RequestRentOfficeSpaceCharges
    {
        public int RentOfficeSpaceID { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public int? SacCodeId { get; set; }
        public decimal? Rate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
