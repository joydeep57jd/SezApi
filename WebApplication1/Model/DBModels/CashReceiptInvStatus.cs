using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SezApi.Model.DBModels
{
    [Table("cashreceiptinvstatus")]
    public class CashReceiptInvStatus
    {
        [Key]
        public int CashRcptInvStatId { get; set; }
        public int? CashReceiptId { get; set; }
        public string ReceiptNo { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public int? PartyId { get; set; }
        public int? InvoiceId { get; set; }
        public decimal? Amount { get; set; }
        public string ModeStatus { get; set; }
    }
}
