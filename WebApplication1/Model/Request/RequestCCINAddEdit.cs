namespace SezApi.Model.Request
{
	public class RequestCCINAddEdit
	{
		public int CCINId { get; set; } 
		public string? CCINNo { get; set; }
		public DateTime? CCINDate { get; set; }
		public string? SBNo { get; set; }
		public DateTime? SBDate { get; set; }
		public int? SBType { get; set; }
		public int? ExporterId { get; set; }
		public int? ShippingLineId { get; set; }
		public int? CHAId { get; set; }
		public string? ConsigneeName { get; set; }
		public string? ConsigneeAdd { get; set; }
		public int? CountryId { get; set; }
		public int? StateId { get; set; }
		public int? CityId { get; set; }
		public int? PortOfLoadingId { get; set; }
		public string? PortOfDischarge { get; set; }
		public int? Package { get; set; }
		public decimal? Weight { get; set; }
		public decimal? FOB { get; set; }
		public int? CommodityId { get; set; }
		public int? CreatedBy { get; set; }
		public int? UpdatedBy { get; set; }
		public int? InvoiceId { get; set; }
		public string? Remarks { get; set; }
		public int IsApproved { get; set; } 
		public int? ApprovedBy { get; set; }
		public DateTime? ApprovedDate { get; set; }
		public int? CargoType { get; set; }
		public int? GodownId { get; set; }
		public string? GodownName { get; set; }
		public int? PortofDestId { get; set; }
		public int? OTHr { get; set; }
		public int IsCancelled { get; set; } 
		public int? EximappID { get; set; }
		public string? PackageType { get; set; }
		public string? PackUQCCode { get; set; }
		public string? PackUQCDesc { get; set; }
		public int SEZ { get; set; } 
	}
}
