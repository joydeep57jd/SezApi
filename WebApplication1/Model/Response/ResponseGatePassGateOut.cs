namespace SezApi.Model.Response
{
    public class ResponseGatePassGateOut
    {
        public string GatePassNo { get; set; }
        public string VehicleNo { get; set; }
        public string Importer { get; set; }
        public string ShipplingLine { get; set; }
        public DateTime? GatePassDateTime { get; set; }
        public string ContainerNo { get; set; }
        public string ContainerSize { get; set; }
        public string CustomerSealNo { get; set; }
        public string CHAName { get; set; }
        public string BoeNo { get; set; }
        public string DBLNo { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime? GatePassValidity { get; set; }
    }
}
