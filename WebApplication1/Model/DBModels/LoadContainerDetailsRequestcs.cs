using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.DBModels
{
	[Table("LoadContainerRequestDetails")]
	public class LoadContainerRequestDetails
	{
		[Key]
		public int LoadContReqDetlId { get; set; }
		public int LoadContReqId { get; set; }
		public int? ExporterId { get; set; }
		public int? ShippingLineId { get; set; }
		public string? ContainerNo { get; set; }
		public string? Size { get; set; }
		public int? Reefer { get; set; } 
		public int? IsInsured { get; set; }
		public string? ShippingBillNo { get; set; }
		public DateTime? ShippingBillDate { get; set; }
		public int? CommodityId { get; set; }
		public int? CargoType { get; set; } 
		public string? CargoDescription { get; set; }
		public decimal? GrossWt { get; set; } 
		public int? NoOfUnits { get; set; } 
		public decimal? FobValue { get; set; } 
		public string? PackUQCCode { get; set; }
		public string? PackUQCDesc { get; set; }
		public int? SEZ { get; set; } 
		public int? SFSend { get; set; }
		public string? EquipmentSealType { get; set; }
		public string? EquipmentStatus { get; set; }
		public string? EquipmentQUC { get; set; }
		public string? PackageType { get; set; }
		public string? ContLoadType { get; set; }
		public string? CustomSeal { get; set; }
		public int? packetsFrom { get; set; }
		public int? packetsTo { get; set; } 
	}
}
