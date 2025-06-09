using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.DBModels
{
    [Table("mstmovementcharge")]
    public class MovementCharge
    {
        [Key]
       public int MovementChargeId { get; set; }
       public string MovementBy { get; set; }
       public string Origin { get; set; }
       public string MovementVia { get; set; }
       public string Size { get; set; }
       public int? CargoType { get; set; }
        public decimal? MovementRate { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
