using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SezApi.Model.DBModels
{
    [Table("GodownInvoiceCharges")]
    
    public class GodownInvoiceChargescs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GodownInvoiceChargeId { get; set; }

        public int ChargesTypeId { get; set; }

        [Column("InoviceId")] // Keep DB spelling, but you may consider correcting it in DB later
        public int InvoiceId { get; set; }

        public int? OperationId { get; set; }

        [StringLength(20)]
        public string? Clause { get; set; }

        [StringLength(45)]
        public string? ChargeType { get; set; }

        [StringLength(500)]
        public string? ChargeName { get; set; }

        [StringLength(10)]
        public string? SACCode { get; set; }

        public int? Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Rate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Amount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Discount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Taxable { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? IGSTPer { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? IGSTAmt { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? CGSTPer { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? CGSTAmt { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? SGSTPer { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? SGSTAmt { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Total { get; set; }
    }
}
