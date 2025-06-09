namespace SezApi.Model.Request
{
    public class RequestMstRailFreightFees
    {
        public int RailFreightId { get; set; }
        public byte? ContainerType { get; set; }
        public byte? CommodityType { get; set; }
        public byte? OperationType { get; set; }
        public byte? Reefer { get; set; }
        public decimal? Rate { get; set; }
        public string ContainerSize { get; set; }
        public int? Port { get; set; }
        public int? LocationId { get; set; }
        public decimal? FromMetric { get; set; }
        public decimal? ToMetric { get; set; }
        public int? BranchId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
