using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.DBModels
{
    [Table("impdestuffingentryhdr")]
    public class ImpDestuffingEntryHdr
    {
        [Key]
        public int DestuffingEntryId { get; set; }
        public string? DestuffingEntryNo { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DestuffingEntryDate { get; set; }
        public int? TallySheetId { get; set; }
        public int? ContainerId { get; set; }
        public string? ContainerNo { get; set; }
        public string? Size { get; set; }
        public string? CFSCode { get; set; }
        public int? ShippingLineId { get; set; }
        public int? CHAId { get; set; }
        public string? Rotation { get; set; }
        public int? DeliveryType { get; set; }
        public int? DOType { get; set; }
        public int? GodownId { get; set; }
        public int? BranchId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? CargoDelivery { get; set; }
    }

    [Table("impdestuffingentrydtl")]
    public class ImpDestuffingEntryDtl
    {
        [Key]
        public int DestuffingEntryDtlId { get; set; }
        public int? DestuffingEntryId { get; set; }
        public int? TallySheetDtlId { get; set; }
        public string? OblHblNo { get; set; }
        public DateTime? OblHblDate { get; set; }
        public int? CommodityId { get; set; }
        public string? BOENo { get; set; }
        public DateTime? BOEDate { get; set; }
        public string? LineNo { get; set; }
        public string? CargoDescription { get; set; }
        public int? NoOfPackages { get; set; }
        public int? ReceivedPackages { get; set; }
        public string? UOM { get; set; }
        public decimal? GrossWeight { get; set; }
        public decimal? DestuffWeight { get; set; }
        public decimal? CIFValue { get; set; }
        public decimal? GrossDuty { get; set; }
        public decimal? Area { get; set; }
        public string? GodownWiseLocationIds { get; set; }
        public string? GodownWiseLctnNames { get; set; }
        public string? Remarks { get; set; }
        public DateTime? OblWiseDestuffingDate { get; set; }
        public int? CargoType { get; set; }
        public int? LocationId { get; set; }
        public string? Location { get; set; }
    }
}
