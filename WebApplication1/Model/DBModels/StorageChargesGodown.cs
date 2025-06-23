using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.DBModels
{
    [Table("mstStorageChargesGodown")]
    public class StorageChargesGodown
    {
        [Key]
        public int StorageChargeID { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public int? SacCodeId { get; set; }
        public int? StorageForId { get; set; }
        public string? StorageForName { get; set; }
        public int? AreaTypeId { get; set; }
        public string? AreaTypeName { get; set; }
        public int? BasisId { get; set; }
        public string? BasisName { get; set; }
        public decimal? RatePerSqmWeek { get; set; }
        public decimal? RatePerSqmMonth { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } 
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
