using System.ComponentModel.DataAnnotations;

namespace SezApi.Model.Response
{
	public class ResponseGetContainerlistByGetEntry
	{
		[Key]
		public string ContainerNo { get; set; }
	}
}
