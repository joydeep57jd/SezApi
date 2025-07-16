using System.ComponentModel.DataAnnotations;

namespace SezApi.Model.Response
{
	public class GatePassDetailsFlat
	{
		[Key]
        public string InvoiceNo { get; set; } = string.Empty;
        public DateTime DeliveryDate { get; set; }

        public int ChaId { get; set; }
        public int ImporterExporterId { get; set; }
        public string ImporterExporterName { get; set; } = string.Empty;

        public int ShippingLineId { get; set; }
        public string ShippingLine { get; set; } = string.Empty;

        public string Remarks { get; set; } = string.Empty;

        public string ContainerNo { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public string CargoDescription { get; set; } = string.Empty;
        public string CargoType { get; set; } = string.Empty;

        public string VehichleNo { get; set; } = string.Empty;
        public int NoofPackages { get; set; }
        public decimal GrossWeight { get; set; }

        public string DLocation { get; set; } = string.Empty;
        public int PortId { get; set; }

        public int ExitIdDtls { get; set; }
        public int ExitidHeader { get; set; }

        public string DepositorName { get; set; } = string.Empty;
        public string Reefer { get; set; } = string.Empty;
        public string CfsCode { get; set; } = string.Empty;
    }

	public class GatePassDetailsStructured
	{
		public string? InvoiceNo { get; set; }
		public DateTime? DeliveryDate { get; set; }
		public int? ChaId { get; set; }
		public int? ImporterExporterId { get; set; }
		public string? ImporterExporterName { get; set; }
		public int? ShippingLineId { get; set; }
		public string? ShippingLine { get; set; }
		public string? Remarks { get; set; }

		public List<GatePassContainerDto> ContainersDetails { get; set; }
	}
	public class GatePassContainerDto
	{
		public string? ContainerNo { get; set; }
		public string? Size { get; set; }
		public string? CargoDescription { get; set; }
		public string? CargoType { get; set; }
		public string? VehichleNo { get; set; }  
		public int? NoofPackages { get; set; }
		public decimal? GrossWeight { get; set; }
		public string? DLocation { get; set; }    
		public int? PortId { get; set; }

		public int? ExitIdDtls { get; set; }
		public int? ExitidHeader { get; set; }
		public string? DepositorName { get; set; }
		public string? Reefer { get; set; }
		public string? CfsCode { get; set; }
	}

}
