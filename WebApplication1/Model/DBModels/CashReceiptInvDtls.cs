using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SezApi.Model.DBModels
{
    [Table("cashreceiptinvdtls")]
    public class CashReceiptInvDtls
    {
        [Key]
        public int CashRcptInvDtlsId { get; set; }
        public int? CashReceiptId { get; set; }
        public int? PartyId { get; set; }
        public int? InvoiceId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public decimal? InvoiceAmt { get; set; }
        public decimal? DueAmt { get; set; }
        public decimal? AdjustmentAmt { get; set; }
    }
}
