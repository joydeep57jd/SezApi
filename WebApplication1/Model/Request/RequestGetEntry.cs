namespace SezApi.Model.Request
{
    public class RequestGetEntry
    {
        public int EntryId { get; set; }
        public string OperationName { get; set; }
        public string ReferenceNo { get; set; }
        public string OperationType { get; set; }
        public string DeliveryType { get; set; }
        public int PartyId { get; set; }
        public string ShippingLine { get; set; }
        public string ContainerType { get; set; }
        public string ContainerNo { get; set; }
        public string Size { get; set; }
        public string MaterialType { get; set; }
        public string VehicleNo { get; set; }
        public string DriverName { get; set; }
        public string DriverLicenseNo { get; set; }
        public string Remarks { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UpdatedBy { get; set; }
    }

}
