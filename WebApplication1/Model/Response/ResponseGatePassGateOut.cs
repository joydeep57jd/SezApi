namespace SezApi.Model.Response
{
    public class ResponseGatePassGateOut
    {
		public int? GatePassId { get; set; }
		public int? GatepassDtlId { get; set; }
		public string? GatePassNo { get; set; }
        public string? VehicleNo { get; set; }
        public string? Importer { get; set; }
        public string? ShipplingLine { get; set; }
        public DateTime? GatePassDateTime { get; set; }
        public string? ContainerNo { get; set; }
        public string? size { get; set; }
        public string? CustomerSealNo { get; set; }
        public string? CHAName { get; set; }
        public string? BoeNo { get; set; }
        public string? DBLNo { get; set; }
        public string? InvoiceNo { get; set; }
        public DateTime? GatePassValidity { get; set; }

		public decimal? ElwbTareWeight { get; set; }
		public decimal? ElwbCargoWeight { get; set; }

		public string? CargoDescription { get; set; }
		public int? CargeType { get; set; }
		public int? NoOfUnits { get; set; }
		public decimal? Weight { get; set; }
		public string? Location { get; set; }
		public string? PortOfDispatch { get; set; }
		public int? IsReefer { get; set; }
	}
}
