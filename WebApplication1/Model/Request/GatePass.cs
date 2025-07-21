using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.Request
{
    
        [Table("GatePass")]
    public class GatePass
    {
        [Key]
        public int GatePassId { get; set; }
        public string? GatePassNo { get; set; }
        public DateTime? GatePssDate { get; set; }
        public DateTime? ExpDate { get; set; }
        public string? ChaName { get; set; }
        public string? ImpExpName { get; set; }
        public string? ShippingLineName { get; set; }
        public string? Remarks { get; set; }
        public int? InvoiceId { get; set; }
        public string? InvoiceNo { get; set; }
        public int? BranchId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public string? IsCancelled { get; set; }
        public DateTime? CancelledOn { get; set; }
        public int? CancelledBy { get; set; }
        public DateTime? DepartureDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public int? DPMsgStatus { get; set; }
        public int? DPAmendStatus { get; set; }
        public int? MsgFlag { get; set; }
        public string? FileName { get; set; }
        public int? FileCode { get; set; }

    }
}
