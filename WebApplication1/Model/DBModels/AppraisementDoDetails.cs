using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.DBModels
{
    [Table("AppraisementDoDetails")]
    public class AppraisementDoDetails
    {
        [Key]
        public int Id { get; set; }
        public string? DoIssuedBy { get; set; }
        public string? CargosDeliveredTo { get; set; }
        public string? ValidType { get; set; }
        public DateTime? DoValidDate { get; set; }
        public int? CustomAppraisementId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }

    }
}
