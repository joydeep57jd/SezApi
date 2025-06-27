using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.DBModels
{
	[Table("ContainerStuffingHeader")]
	public class ContainerStuffingHeader
	{
		[Key]
		public int StuffingReqId { get; set; }
		public bool? ByTrain { get; set; }
		public bool? ByRoad { get; set; }
		public string StuffingReqNo { get; set; }
		public int? StuffingReqNoId { get; set; }
		public DateTime? RequestDate { get; set; }
		public string StuffingNo { get; set; }
		public DateTime? StuffingDate { get; set; }	
		public string ContainerNo { get; set; }	
		public string ICDCode { get; set; }
		public string ContainerSize { get; set; }
		public bool? FCL { get; set; }
		public bool? LCL { get; set; }	
		public string POD { get; set; }
		public int? PODId { get; set; }		
		public string Origin { get; set; }
		public int? OriginId { get; set; }		
		public string ContPOL { get; set; }
		public int? ContPOLId { get; set; }	
		public string Via { get; set; }
		public int? ViaId { get; set; }		
		public string ShippingLine { get; set; }		
		public string ShippingSeal { get; set; }
		public string CustomSeal { get; set; }
		public string FinalDestinationLocation { get; set; }
		public int? FinalDestinationLocationId { get; set; }
		public string EquipmentSealType { get; set; }
		public int? EquipmentSealTypeId { get; set; }
		public string EquipmentStatus { get; set; }
		public int? EquipmentStatusId { get; set; }	
		public string EquipmentQUC { get; set; }
		public int? EquipmentQUCId { get; set; }
		public string Remarks { get; set; }	
		public string SEZ { get; set; }
		public bool? DirectStuffing { get; set; }
	}
}

