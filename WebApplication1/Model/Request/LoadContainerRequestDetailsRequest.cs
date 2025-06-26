namespace SezApi.Model.Request
{
	public class RequestLoadContainerRequestDetails
	{
		public int LoadContReqId { get; set; }
		public int? ExporterId { get; set; }
		public int? ShippingLineId { get; set; }
		public string? ContainerNo { get; set; }
		public string? Size { get; set; }
		public int? Reefer { get; set; } = 0;
		public int? IsInsured { get; set; } = 0;
		public string? ShippingBillNo { get; set; }
		public DateTime? ShippingBillDate { get; set; }
		public int? CommodityId { get; set; }
		public int? CargoType { get; set; } = 2;
		public string? CargoDescription { get; set; }
		public decimal? GrossWt { get; set; } = 0;
		public int? NoOfUnits { get; set; } = 0;
		public decimal? FobValue { get; set; } = 0;
		public string? PackUQCCode { get; set; }
		public string? PackUQCDesc { get; set; }
		public int? SEZ { get; set; } = 0;
		public int? SFSend { get; set; } = 0;
		public string? EquipmentSealType { get; set; }
		public string? EquipmentStatus { get; set; }
		public string? EquipmentQUC { get; set; }
		public string? PackageType { get; set; }
		public string? ContLoadType { get; set; }
		public string? CustomSeal { get; set; }
		public int? packetsFrom { get; set; } = 0;
		public int? packetsTo { get; set; } = 0;
	}
}
