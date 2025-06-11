using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.DBModels
{
    [Table("mstGodown")]
    public class GoDown
    {
        public int GodownId { get; set; } = 0;
        public string GodownName { get; set; }
        public string LocationAlias { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
