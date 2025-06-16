using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.DBModels
{
    [Table("CustomAppraisementApplicationHeader")]
    public class CustomAppraisementApplicationHeader
    {
        [Key]
        public int ID { get; set; }
        public string? AppraisementNo { get; set; }
        public DateTime? AppraisementDate { get; set; }
        public int? ShippingLineId { get; set; }
        public int? CHAId { get; set; }
        public string? Vessel { get; set; }
        public string? Voyage { get; set; }
        public string? Rotation { get; set; }
        public string? DeliveryType { get; set; }
        public string? DOStatus { get; set; }
        public string? AppraisementStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
