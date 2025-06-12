using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.DBModels
{
    [Table("OBLEntry")]
    public class OBLEntry
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string ContainerCBTType { get; set; }

        [StringLength(100)]
        public string ContainerCBTNo { get; set; }

        [StringLength(50)]
        public string ContainerCBTSize { get; set; }

        [StringLength(100)]
        public string IGMNo { get; set; }

        public DateTime? IGMDate { get; set; }

        [StringLength(100)]
        public string TPNo { get; set; }

        public DateTime? TPDate { get; set; }

        [StringLength(50)]
        public string MovementType { get; set; }
        public int? Port { get; set; }
        public int? Country { get; set; }

        [StringLength(100)]
        public string ShippingLine { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
