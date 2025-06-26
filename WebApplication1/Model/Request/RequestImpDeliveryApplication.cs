using SezApi.Model.DBModels;
namespace SezApi.Model.Request
{
    public class RequestImpDeliveryApplication
    {
        public ImpDeliveryApplicationHdr ImpDeliveryApplicationHdr {  get; set; }
        public List<ImpDeliveryApplicationDtl> ImpDeliveryApplicationDtl { get; set; }
    }
}
