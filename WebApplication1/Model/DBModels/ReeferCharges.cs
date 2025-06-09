using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.DBModels
{
    [Table("mstreeferchrg")]
    public class ReeferCharges
    {
        [Key]
        public int ReeferChrgId { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public decimal? ElectricityCharge { get; set; }
        public string SacCode { get; set; }
        public string ContainerSize { get; set; } 
        public int? CreatedBy { get; set; } 
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
