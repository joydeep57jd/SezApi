using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.DBModels
{
    [Table("exitthroughgatedetails")]
    public class ExitThroughGateDetails
    {
        [Key]
        public int ExitIdDtls { get; set; }
        public int? ExitIdHeader { get; set; } 
        public string? ContainerNo { get; set; }
        public string? Size { get; set; }
        public int? Reefer { get; set; }
        public string? ShippingLine { get; set; }
        public string? CHAName { get; set; }
        public string? CargoDescription { get; set; }
        public int? CargoType { get; set; }
        public string? VehicleNo { get; set; }
        public int? NoOfPackages { get; set; } 
        public decimal? GrossWeight { get; set; }
        public string? DepositorName { get; set; }
        public string? Remarks { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? ShippingLineID { get; set; }
        public string? CFSCode { get; set; }
        public DateTime? ExpectedArrivalDateTime { get; set; }

    }
}
