using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.DBModels
{
    [Table("mstGodown")]
    public class GoDown
    {
        public int GodownId { get; set; } = 0;
        public string GodownName { get; set; }
        public string LocationAlias { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }

    [Table("godowninvoice")]
    public class GodownInvoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GodownInvId { get; set; }

        public bool? IsTaxInvoice { get; set; }

        public bool? IsBillOfSupply { get; set; }

        [StringLength(100)]
        public string? InvoiceNo { get; set; }

        public DateTime? DeliveryDate { get; set; }

        [StringLength(100)]
        public string? ApplicationNo { get; set; }

        public DateTime? InvoiceDate { get; set; }

        [StringLength(200)]
        public string? PartyName { get; set; }

        public int? PartyId { get; set; }

        [StringLength(200)]
        public string? PayeeName { get; set; }

        public int? PayeeId { get; set; }

        [StringLength(50)]
        public string? GSTNo { get; set; }

        [StringLength(50)]
        public string? OTHours { get; set; }

        [StringLength(100)]
        public string? PaymentMode { get; set; }

        public string? Remarks { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "bit")]
        public bool? IsImport { get; set; } = true;
    }
}
