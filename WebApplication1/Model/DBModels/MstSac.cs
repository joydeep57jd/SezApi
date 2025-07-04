using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.DBModels
{
    [Table("mstsac")]
    public class MstSac
    {
        [Key]
        public int SacId { get; set; }
        public int? BranchId { get; set; }
        public string? SacCode { get; set; }
        public string? Description { get; set; }
        public decimal? Gst { get; set; }
        public decimal? Cess { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }   
}
