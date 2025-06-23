using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.DBModels
{
    [Table("mstTransportationCharges")]
    public class TransportationCharges
    {
        [Key]
        public int TransportationChargesID { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public int? SacCodeId { get; set; }
        public int? ApplicableForId { get; set; }
        public string? ApplicableForName { get; set; }
        public int? ValueId { get; set; }
        public decimal? Rate { get; set; }
        public decimal? AdditionalRatePerPacket { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; } 
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
