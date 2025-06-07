namespace SezApi.Model.Request
{
    public class HTChargesRequest
    {
        public int? HTChargesID { get; set; } 
        public int? OperationId { get; set; }
        public int? ContainerType { get; set; }
        public int? Type { get; set; }
        public int? Size { get; set; }
        public decimal? MaxDistance { get; set; }
        public int? CommodityType { get; set; }
        public string ContainerLoadType { get; set; }
        public string TransportFrom { get; set; } 
        public string EximType { get; set; }
        public decimal? RateCWC { get; set; }
        public decimal ContractorRate { get; set; } 
        public DateTime? EffectiveDate { get; set; }
        public int BranchId { get; set; } 
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } 
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
