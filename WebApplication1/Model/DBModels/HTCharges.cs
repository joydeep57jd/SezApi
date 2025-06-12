using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.DBModels
{
    [Table("msthtcharges")]
    public class HTCharges
    {
        [Key]
        public int HTChargesID { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public int? SacCodeId { get; set; }
        public string OperationType { get; set; }
        public decimal? RateperPacket { get; set; }
        public string WeightForAdditionalCharges { get; set; }
        public decimal? RateForAdditionalCharges { get; set; }
        public decimal? MinimumRate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
