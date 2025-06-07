using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.DBModels
{
    [Table("mstentryfees")]
    public class MstEntryFee
    {
        [Key]
        public int EntryFeeId { get; set; }
        public byte? ContainerType { get; set; }
        public byte? CommodityType { get; set; }
        public byte? OperationType { get; set; }
        public byte? Reefer { get; set; }
        public decimal? Rate { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public string ContainerSize { get; set; }
        public string SacCode { get; set; }
        public int? BranchId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? WeightSlab { get; set; }
    }
}
