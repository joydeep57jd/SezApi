using System.ComponentModel.DataAnnotations;

namespace SezApi.Model.DBModels
{
    public class mststoragecharge
    {
        [Key]
        public int StorageChargeId { get; set; }
        public int? BranchId { get; set; }
        public short? WarehouseType { get; set; }
        public short? ChargeType { get; set; }
        public decimal? RateSqMPerWeek { get; set; }
        public decimal? RateSqMeterPerMonth { get; set; }
        public decimal? RateMeterPerDay { get; set; }
        public decimal? RateCubMeterPerDay { get; set; }
        public decimal? RateCubMeterPerWeek { get; set; }
        public decimal? RateCubMeterPerMonth { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public int? DaysRangeFrom { get; set; }
        public int? DaysRangeTo { get; set; }
        public string SacCode { get; set; }
        public int? CommodityType { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? SurCharge { get; set; }
    }
}
