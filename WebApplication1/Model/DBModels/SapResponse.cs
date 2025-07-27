using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.DBModels
{
    [Table("sapresponse")]
    public class SapResponse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? InvoiceId { get; set; }

        [MaxLength(45)]
        public string InvoiceNo { get; set; }

        [MaxLength(45)]
        public string SAP_DOC_NUMBER { get; set; }

        [MaxLength(45)]
        public string REF_DOC_NO { get; set; }

        [MaxLength(45)]
        public string STATUS { get; set; }

        [MaxLength(200)]
        public string REMARK { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int CreatedBy { get; set; } = 0;

        public DateTime? CreatedOn { get; set; }

        [MaxLength(45)]
        public string Module { get; set; }

        [MaxLength(50)]
        public string InvoiceType { get; set; }
    }
}
