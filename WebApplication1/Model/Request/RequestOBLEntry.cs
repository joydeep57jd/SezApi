using System.ComponentModel.DataAnnotations;

namespace SezApi.Model.Request
{
    public class RequestOBLEntry
    {
        public int Id { get; set; }
        public string ContainerCBTType { get; set; }
        public string ContainerCBTNo { get; set; }
        public string ContainerCBTSize { get; set; }
        public string IGMNo { get; set; }
        public DateTime? IGMDate { get; set; }
        public string TPNo { get; set; }
        public DateTime? TPDate { get; set; }
        public string MovementType { get; set; }
        public int? Port { get; set; }
        public int? Country { get; set; }
        public string ShippingLine { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public List<RequestOblEntryAddDtl> requestOblEntryAddDtls { get; set; }
    }
}
