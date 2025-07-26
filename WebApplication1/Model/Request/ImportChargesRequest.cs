namespace SezApi.Model.Request
{
    public class ImportChargesRequest
    {
        public string ContainerOBLList { get; set; }
        public int PartyId { get; set; }
        public int TypeOfCharge { get; set; }
        public  bool isYardInvoice { get; set; }
    }
}
