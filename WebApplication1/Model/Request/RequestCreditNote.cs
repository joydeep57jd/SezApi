using SezApi.Model.DBModels;
namespace SezApi.Model.Request
{
    public class RequestCreditNote
    {
        public long CreditNoteId { get; set; }
        public string? CreditNoteNo { get; set; }
        public DateTime? CreditNoteDate { get; set; }
        public string? InvoiceNo { get; set; }
        public long? PartyId { get; set; }
        public string? Remarks { get; set; }

        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }

        public List<CreditNoteDetail> CreditNoteDetailList { get; set; } = new List<CreditNoteDetail>();
    }
}
