using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.DBModels
{
    [Table("mstfumigationcharge")]
    public class FumigationCharge
    {
        [Key]
        public int FumigationChargeId { get; set; }
        public string? ChargesFor { get; set; }
        public int? ContainerSize { get; set; }
        public decimal? FromWeight { get; set; }
        public decimal? ToWeight { get; set; }
        public decimal? Rate { get; set; }
    }
}
