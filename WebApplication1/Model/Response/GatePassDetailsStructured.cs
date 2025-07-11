using System.ComponentModel.DataAnnotations;

namespace SezApi.Model.Response
{
	public class GatePassDetailsFlat
	{
		[Key]
		public string InvoiceNo { get; set; }               // YI.InvoiceNo
		public DateTime? DeliveryDate { get; set; }         // YI.DeliveryDate

		public int? ChaId { get; set; }                      // LCR.CHAId or 0
		public int? ImporterExporterId { get; set; }         // LCRD.ExporterId or 0
		public string? ImporterExporterName { get; set; }    // OBLA.Importer_Name or ''

		public int? ShippingLineId { get; set; }             // LCRD.ShippingLineId or 0
		public string? ShippingLine { get; set; }            // OBLE.ShippingLine or ''

		public string? Remarks { get; set; }                 // YI.Remarks

		public string? ContainerNo { get; set; }             // LCRD.ContainerNo or OBLE.ContainerCBTNo
		public string? Size { get; set; }                    // LCRD.Size or OBLE.ContainerCBTSize

		public string? CargoDescription { get; set; }        // LCRD.CargoDescription or OBLA.Cargo_Desc
		public string? CargoType { get; set; }               // LCRD.CargoType or OBLA.Cargo_Type

		public string? VehichleNo { get; set; }              // Always '' from SP
		public int? NoofPackages { get; set; }              // LCRD.NoOfUnits or OBLA.No_of_PKG
		public decimal? GrossWeight { get; set; }           // LCRD.GrossWt or OBLA.GR_WT_Kg

		public string? DLocation { get; set; }               // LCR.FinalDestinationLocation or ''
		public int? PortId { get; set; }                    // OBLE.Port or 0
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
		public string DLocation { get; set; }    
		public int? PortId { get; set; }
	}

}
