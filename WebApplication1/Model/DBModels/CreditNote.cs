using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SezApi.Model.DBModels
{
    [Table("CreditNote")]
    public class CreditNote
    {
        [Key]
        public int CreditNoteId { get; set; }
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
        public string? SAP_DOC_NUMBER { get; set; }
        public int? IsSAP { get; set; }
    }

    [Table("CreditNoteDetail")]
    public class CreditNoteDetail
    {
        [Key]
        public int CreditNoteDetailId { get; set; }
        public int? CreditNoteId { get; set; }
        public int SlNo { get; set; }
        public string Particulars { get; set; }
        public string SAC { get; set; }
        public decimal Value { get; set; }
        public decimal ReturnValue { get; set; }

        public decimal CGSTPercent { get; set; }
        public decimal CGSTAmount { get; set; }
        public decimal SGSTPercent { get; set; }
        public decimal SGSTAmount { get; set; }
        public decimal IGSTPercent { get; set; }
        public decimal IGSTAmount { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal RoundOff { get; set; }
        public decimal GrandTotal { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
