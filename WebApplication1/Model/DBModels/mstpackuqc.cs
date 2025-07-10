using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.DBModels
{
	[Table("mstpackuqc")]
	public class mstpackuqc
	{
		[Key]
		public int Id { get; set; }
		public string? UQCCode { get; set; }
		public string? UQCDescription { get; set; }
	}
}
