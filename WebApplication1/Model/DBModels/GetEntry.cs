using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.DBModels
{
    [Table("gateentry")]
    public class GateEntry
    {
        [Key]
        public int EntryId { get; set; }

        [StringLength(50)]
        public string? OperationName { get; set; }

        [StringLength(50)]
        public string? ReferenceNo { get; set; }

        [StringLength(50)]
        public string? OperationType { get; set; }

        [StringLength(50)]
        public string? DeliveryType { get; set; }

        public int? PartyId { get; set; }

        public int? ShippingLine { get; set; }

        [StringLength(50)]
        public string? ContainerType { get; set; }

        [StringLength(50)]
        public string? ContainerNo { get; set; }

        [StringLength(20)]
        public string? Size { get; set; }

        [StringLength(100)]
        public string? MaterialType { get; set; }

        [StringLength(50)]
        public string? VehicleNo { get; set; }

        [StringLength(100)]
        public string? DriverName { get; set; }

        [StringLength(50)]
        public string? DriverLicenseNo { get; set; }

        public string? Remarks { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? UpdatedBy { get; set; }
    }
}
