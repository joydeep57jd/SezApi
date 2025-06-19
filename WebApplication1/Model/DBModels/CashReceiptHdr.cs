using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SezApi.Model.DBModels
{
    [Table("cashreceipthdr")]
    public class CashReceiptHdr
    {
        [Key]
        public int CashReceiptId { get; set; }
        public int? BranchId { get; set; }
        public int? AutoCashRcptNo { get; set; }
        public string? ReceiptNo { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public int? InvoiceId { get; set; }
        public int? PartyId { get; set; }
        public int? PayByPdaId { get; set; }
        public string? PayeeName { get; set; }
        public byte? PdaAdjust { get; set; }
        public string? FolioNo { get; set; }
        public decimal? PdaAdjustedAmount { get; set; }
        public decimal? PdaOpening { get; set; }
        public decimal? PdaClosing { get; set; }
        public decimal? TotalPaymentReceipt { get; set; }
        public decimal? TdsAmount { get; set; }
        public decimal? InvoiceValue { get; set; }
        public string? CompYear { get; set; }
        public string? Remarks { get; set; }
        public int? PdaAccountDetailsID { get; set; }
        public string? FromPDA { get; set; }
        public string? CashReceiptHtml { get; set; }
        public int? IsCancelled { get; set; }
        public string? CancelledReason { get; set; }
        public DateTime? CancelledOn { get; set; }
        public int? CancelledBy { get; set; }
        public string? InvoiceDebitNote { get; set; }
        public decimal? OnlineFacAmt { get; set; }
        public string? Area { get; set; }
        public string? TransId { get; set; }
        public int? IsSAP { get; set; }
        public int? IsSAPRev { get; set; }
        public string? SAP_DOC_NUMBER { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
