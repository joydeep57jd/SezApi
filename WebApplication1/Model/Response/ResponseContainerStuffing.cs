using System.ComponentModel.DataAnnotations;

namespace SezApi.Model.Response
{
	public class ResponseContainerStuffing
	{
		[Key]
		public string? Response { get; set; }  // "OK" or "NOT OK"
		public int? Id { get; set; }
	}
}
