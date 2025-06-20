namespace SezApi.Model.Response
{
    public class ResponseYardInvoiceFlat
    {
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
            public string? PayeeName { get; set; }

            // From YardInvoiceCharges
            public int? YardInvoiceChargeId { get; set; }
            public int? ChargesTypeId { get; set; }
            public int? InoviceId { get; set; }
            public int? OperationId { get; set; }
            public string? Clause { get; set; }
            public string? ChargeType { get; set; }
            public string? ChargeName { get; set; }
            public string? SACCode { get; set; }
            public int? Quantity { get; set; }
            public decimal? Rate { get; set; }
            public decimal? Amount { get; set; }
            public decimal? Discount { get; set; }
            public decimal? Taxable { get; set; }
            public decimal? IGSTPer { get; set; }
            public decimal? IGSTAmt { get; set; }
            public decimal? CGSTPer { get; set; }
            public decimal? CGSTAmt { get; set; }
            public decimal? SGSTPer { get; set; }
            public decimal? SGSTAmt { get; set; }
            public decimal? Total { get; set; }
        

    }
}
