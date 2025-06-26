using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.DBModels
{
	[Table("LoadContainerRequestHeader")]
	public class LoadContainerRequestHeader
	{
		[Key]
		public int LoadContReqId { get; set; }
		public string? LoadContReqNo { get; set; }
		public DateTime? LoadContReqDate { get; set; }
		public int? CHAId { get; set; }
		public int? FinalDestinationLocationID { get; set; }
		public string? FinalDestinationLocation { get; set; }
		public string? Remarks { get; set; }
		public string? Movement { get; set; }
		public string? ExamType { get; set; }
		public int? BranchId { get; set; }
		public int? CreatedBy { get; set; }
		public DateTime? CreatedOn { get; set; }
		public int? UpdatedBy { get; set; }
		public DateTime? UpdatedOn { get; set; }
		public int? IsApproved { get; set; }
		public int? SFMsgStatus { get; set; }
		public string? Origin { get; set; }
		public string? Via { get; set; }
		public string? TransactionType { get; set; }
		public int? SFSend { get; set; }
	}
}
