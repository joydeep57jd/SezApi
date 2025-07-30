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

        public int ChargesTypeId { get; set; }      // NOT NULL in DB
        public int CreditNoteId { get; set; }       // NOT NULL in DB

        public string? ChargeType { get; set; }
        public string? ChargeName { get; set; }
        public string? SACCode { get; set; }

        public int? Quantity { get; set; }

        public decimal? Rate { get; set; }
        public decimal? Inv_Amount { get; set; }
        public decimal? Taxable { get; set; }

        public decimal? IGSTPer { get; set; }
        public decimal? IGSTAmt { get; set; }

        public decimal? CGSTPer { get; set; }
        public decimal? CGSTAmt { get; set; }

        public decimal? SGSTPer { get; set; }
        public decimal? SGSTAmt { get; set; }

        public decimal? Total { get; set; }

        public bool IsActive { get; set; } // NOT NULL in DB
    }
}
