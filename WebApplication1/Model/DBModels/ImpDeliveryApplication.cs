using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.DBModels
{
    [Table("impdeliveryapplicationhdr")]
    public class ImpDeliveryApplicationHdr
    {
        [Key]
        public int DeliveryId { get; set; }
        public string? DeliveryNo { get; set; }
        public int? DestuffingId { get; set; }
        public int? CHAId { get; set; }
        public int? ImporterId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }

    [Table("impdeliveryapplicationdtl")]
    public class ImpDeliveryApplicationDtl
    {
        [Key]
        public int DeliveryDtlId { get; set; }
        public int? DeliveryId { get; set; }
        public int? DestuffingEntryDtlId { get; set; }
        public string? LineNo { get; set; }
        public string? OBL { get; set; }
        public string? CargoDescription { get; set; }
        public int? CommodityId { get; set; }
        public int? NoOfPackages { get; set; }
        public decimal? GrossWt { get; set; }
        public decimal? SQM { get; set; }
        public decimal? CUM { get; set; }
        public decimal? CIF { get; set; }
        public decimal? Duty { get; set; }
        public int? DelNoOfPackages { get; set; }
        public decimal? DelGrossWt { get; set; }
        public decimal? DelSQM { get; set; }
        public decimal? DelCUM { get; set; }
        public decimal? DelCIF { get; set; }
        public decimal? DelDuty { get; set; }
        public string? BOE_NO { get; set; }
        public string? BOE_DATE { get; set; }
        public int? ImporterId { get; set; }
        public int? InvCancel { get; set; } 
    }

}
