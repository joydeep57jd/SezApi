namespace SezApi.Model.Response
{
    public class ResponseAddEditPort
    {
        public int PortId { get; set; }
        public string PortName { get; set; }
        public string PortAlias { get; set; }
        public bool POD { get; set; }
        public int? Country { get; set; }
        public int? State { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CountryName { get; set; } 
        public string StateName { get; set; } 
    }
    public class ResponseInvoiceByPayee
    {
        public List<YardInvoiceSummary> YardInvoice { get; set; }
        public List<GodownInvoiceSummary> GodownInvoice { get; set; }
    }

    public class YardInvoiceSummary
    {
        public int YardInvId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class GodownInvoiceSummary
    {
        public int GodownInvId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
    }

}
