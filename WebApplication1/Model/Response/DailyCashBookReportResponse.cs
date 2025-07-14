using System.ComponentModel.DataAnnotations;
namespace SezApi.Model.Response
{
    public class DailyCashBookReportResponse
    {
        [Key]
        public string CompanyName { get; set; }
        public string GSTNo { get; set; }
        public string StateName { get; set; }
        public string CustomerName { get; set; }
        public string PeriodOfInvoice { get; set; }
        public string HSNCode { get; set; }
        public string InvNo { get; set; }
        public DateTime InvDate { get; set; }

        public decimal ENT_Taxable { get; set; }
        public decimal EXM_Taxable { get; set; }
        public decimal TRP_Taxable { get; set; }
        public decimal INS_Taxable { get; set; }
        public decimal HAN_Taxable { get; set; }

        public decimal Amount { get; set; }

        public decimal CGSTRate { get; set; }
        public decimal CGSTAmt { get; set; }

        public decimal SGSTRate { get; set; }
        public decimal SGSTAmt { get; set; }

        public decimal IGSTRate { get; set; }
        public decimal IGSTAmt { get; set; }

        public decimal Total { get; set; }

        public string PaymentMode { get; set; }
        public string Remarks { get; set; }

        public string CreditNoteNo { get; set; }
        public DateTime CreditNoteDate { get; set; }

        public string InvType { get; set; }
        public string EximTraderName { get; set; }

        public string ChqNo { get; set; }
    }
}
