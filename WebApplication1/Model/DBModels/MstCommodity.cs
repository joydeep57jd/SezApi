using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.DBModels
{
    [Table("MstCommodity")]
    public class MstCommodity
    {
        [Key]
        public int CommodityId { get; set; }

        [MaxLength(255)]
        public string? CommodityName { get; set; }

        [MaxLength(10)]
        public string? CommodityType { get; set; }

        [MaxLength(255)]
        public string? Alias { get; set; }

        public bool IsTaxExempted { get; set; } = false;

        public bool IsFumigationChemical { get; set; } = false;

        public DateTime? CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? UpdatedBy { get; set; }
    }
}
