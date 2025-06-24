using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.DBModels
{
    [Table("exitthroughgateheader")]
    public class ExitThroughGateHeader
    {
        [Key]
        public int ExitIdHeaderId { get; set; }
        public string? GateExitNo { get; set; }
        public DateTime? GateExitDateTime { get; set; }
        public int? GatePassId { get; set; }
        public string? GatePassNo { get; set; }
        public DateTime? GatePassDate { get; set; }
        public DateTime? ExpectedTime { get; set; }
        public string? CBTNo { get; set; }
        public string? Size { get; set; }
        public string? ShippingLine { get; set; }
        public string? CHAName { get; set; }
        public string? CargoDescription { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? BranchId { get; set; }
        public int? MsgFlag { get; set; }
        public string? Actual_File_Name { get; set; }
        public int? RuleCode { get; set; }
        public int? DTMsgStatus { get; set; }
        public int? DTAmendStatus { get; set; }
    }
}
