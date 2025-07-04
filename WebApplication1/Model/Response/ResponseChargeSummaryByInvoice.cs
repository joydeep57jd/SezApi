using System.ComponentModel.DataAnnotations;
namespace SezApi.Model.Response
{
	public class ResponseChargeSummaryByInvoice
	{
		[Key]
		public int? InvoiceId { get; set; }
		public DateTime? InvoiceDate { get; set; }
		public string? InvoiceNo { get; set; }
		public string? PortName { get; set; }
		public string? MaterialType { get; set; }

		public decimal? EntryCharges { get; set; }
		public decimal? ExaminationCharges { get; set; }
		public decimal? TransportationCharges { get; set; }
		public decimal? HandlingCharges { get; set; }

		public decimal? TotalCharges { get; set; }
		public string? Remarks { get; set; }
	}
}
