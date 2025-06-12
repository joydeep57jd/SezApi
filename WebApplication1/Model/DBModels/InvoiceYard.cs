using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.DBModels
{
    [Table("YardInvoice")]
    public class InvoiceYard
    {
        [Key]
        public int YardInvId { get; set; }
        public bool? TaxInvoice { get; set; }
        public bool? BillOfSupply { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public int? ApplicationId { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public int? PartyId { get; set; }
        public int? PayeeId { get; set; }
        public string GSTNo { get; set; }
        public string PaymentMode { get; set; }
        public bool? FactoryDestuffing { get; set; }
        public bool? DirectDestuffing { get; set; }
        public string PlaceOfSupply { get; set; }
        public int? SEZId { get; set; }
        public string OTHours { get; set; }
        public string Container { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
