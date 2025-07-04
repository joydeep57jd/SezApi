using System.ComponentModel.DataAnnotations;

namespace SezApi.Model.Response
{
	public class RegisterOfOutwardSupplyReportResponse
	{
		public string CompanyName { get; set; }
		public string GSTNO { get; set; }
		public string StateName { get; set; }
		public string CustomerName { get; set; }
		public string PeriodOfInvoice { get; set; }
		public string NatureOfInvoice { get; set; }
		public string HSNCode { get; set; }
		public string InvNo { get; set; }
		public DateTime? InvDate { get; set; }
		public decimal? TaxableAmt { get; set; }
		public decimal? CGSTRate { get; set; }
		public decimal? CGSTAmt { get; set; }
		public decimal? SGSTRate { get; set; }
		public decimal? SGSTAmt { get; set; }
		public decimal? IGSTRate { get; set; }
		public decimal? IGSTAmt { get; set; }
		public decimal? Total { get; set; }
		public string PaymentMode { get; set; }
		public string Remarks { get; set; }
		public decimal? RatePerBag { get; set; } 
	}
}
