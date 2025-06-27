using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.DBModels
{
	[Table("ContainerStuffingDetails")]
	public class ContainerStuffingDetails
	{
		[Key]
		public int StuffingDtlId { get; set; }	
		public int StuffingReqId { get; set; }	
		public string ShippingBillNo { get; set; }
		public DateTime? ShippingDate { get; set; }
		public int? CHAId { get; set; }		
		public string CHA { get; set; }
		public string ContainerNo { get; set; }	
		public string Exporter { get; set; }
		public string Consignee { get; set; }
		public string CargoDescription { get; set; }
		public string MarksNo { get; set; }	
		public decimal? Fob { get; set; }	
		public decimal? StuffQuantity { get; set; }
		public decimal StuffWeight { get; set; }
		public bool? Insured { get; set; }	
		public string MCINPCIN { get; set; }
		public string CFSCode { get; set; }
		public int? Size { get; set; }		
		public string StuffingType { get; set; }
	}
}
