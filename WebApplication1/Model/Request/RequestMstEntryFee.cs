namespace SezApi.Model.Request
{
    public class RequestMstEntryFee
    {
        public int EntryFeeId { get; set; }
        public string OperationType { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public int? SacCodeId { get; set; }
        public decimal? RatePerPacket { get; set; }
        public decimal? MinimumRate { get; set; }
        public decimal? MaximumRate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
