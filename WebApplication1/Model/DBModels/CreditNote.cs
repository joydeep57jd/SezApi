using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SezApi.Model.DBModels
{
    [Table("CreditNote")]
    public class CreditNote
    {
        [Key]
        public int CreditNoteId { get; set; }

        public bool? TaxInvoice { get; set; }
        public bool? BillOfSupply { get; set; }

        public string? InvoiceNo { get; set; }  // nvarchar(50)

        public DateTime? CreditNoteDate { get; set; }

        public string? CreditNoteNo { get; set; }  // varchar(50)

        public int? PartyId { get; set; }
        public int? PayeeId { get; set; }

        public string? GSTNo { get; set; }  // nvarchar(50)

        public string? PlaceOfSupply { get; set; }  // nvarchar(50)

        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public string? Remarks { get; set; }  // nvarchar(max)

        public bool? IsYard { get; set; }
        public bool? IsImport { get; set; }

        public string? SAP_DOC_NUMBER { get; set; }  // varchar(45)

        public int? IsSAP { get; set; }

        public decimal? Taxable_Amt { get; set; }
        public decimal? IGST_Amt { get; set; }
        public decimal? CGST_Amt { get; set; }
        public decimal? SGST_Amt { get; set; }
        public decimal? Total_Amt { get; set; }
        public decimal? Roundoff_Amt { get; set; }
        public decimal? Net_Amt { get; set; }
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
