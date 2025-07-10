using System.ComponentModel.DataAnnotations;

namespace SezApi.Model.Response
{
	public class ResponseGetContainerlistByGetEntry
	{
		[Key]
		public string ContainerNo { get; set; }
	}

	public class ResponseGetContainerlistForLoadedContainerRequest
	{
		[Key]
		public string ContainerNo { get; set; }
	}
	public class ResponseGetCLandRno
	{
		[Key]
		public int? LoadContReqId { get; set; }
		public int? LoadContReqDetlId { get; set; }
		public string? LoadContReqNo { get; set; }		
		public string? ContainerNo { get; set; }
		public DateTime? LoadContReqDate { get; set; }
	}
}
