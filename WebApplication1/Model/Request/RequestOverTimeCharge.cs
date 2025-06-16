namespace SezApi.Model.Request
{
    public class RequestOverTimeCharge
    {
        public int OverTimeChargeId { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public int? SACCodeId { get; set; }
        public string? OperationType { get; set; }
        public bool? Holiday { get; set; }
        public string? Time { get; set; }
        public decimal? Rate { get; set; }
        public string? MaxMinHours { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
