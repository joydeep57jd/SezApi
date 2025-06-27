using System.ComponentModel.DataAnnotations;

namespace SezApi.Model.Request
{
	public class RequestGodownInvoice
	{
		public int GodownInvId { get; set; }
		public bool? IsTaxInvoice { get; set; }
		public bool? IsBillOfSupply { get; set; } 
		public string? InvoiceNo { get; set; }

		public DateTime? DeliveryDate { get; set; }
		public string? ApplicationNo { get; set; }

		public DateTime? InvoiceDate { get; set; }
		public string? PartyName { get; set; }
		public int? PartyId { get; set; }
		public string? PayeeName { get; set; }
		public int? PayeeId { get; set; }
		public string? GSTNo { get; set; }
		public string? OTHours { get; set; }
		public string? PaymentMode { get; set; }
		public string? Remarks { get; set; }
		public string? jsonData { get; set; }
		public int? CreatedBy { get; set; }
		public int? UpdatedBy { get; set; }
		public DateTime? CreatedDate { get; set; }	
		public DateTime? UpdatedDate { get;	set;}
	}
}
