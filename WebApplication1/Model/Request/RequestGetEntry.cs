namespace SezApi.Model.Request
{
    public class RequestGetEntry
    {
        public int? EntryId { get; set; }
        public string CFSCode { get; set; }
        public string GateInNo { get; set; }
        public DateTime? EntryDateTime { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime? ReferenceDate { get; set; }
        public int? ShippingLineId { get; set; }
        public string ShippingLine { get; set; }
        public string CHAName { get; set; }
        public string ContainerNo { get; set; }
        public string Size { get; set; }
        public bool? Reefer { get; set; } // mapped from tinyint
        public string CustomSealNo { get; set; }
        public string ShippingLineSealNo { get; set; }
        public string VehicleNo { get; set; }
        public string ChallanNo { get; set; }
        public string CargoDescription { get; set; }
        public int? CargoType { get; set; }
        public int? NoOfPackages { get; set; }
        public decimal? GrossWeight { get; set; }
        public string DepositorName { get; set; }
        public string Remarks { get; set; }
        public byte? TransportMode { get; set; } // mapped from tinyint
        public string ContainerLoadType { get; set; }
        public char? TransportFrom { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string ContainerNo1 { get; set; }
        public int? BranchId { get; set; }
        public int? FormOneDetailId { get; set; }
        public string ContainerType { get; set; }
        public string OperationType { get; set; }
        public string DisplayCfs { get; set; }
        public string CHAId { get; set; }
        public int? CBT { get; set; }
        public string TPNo { get; set; }
        public DateTime? SystemDateTime { get; set; }
        public decimal? TareWeight { get; set; }
        public int? MsgFlag { get; set; }
        public int? ActualPackages { get; set; }
        public string FileName { get; set; }
        public int? FileCode { get; set; }
    }

}
