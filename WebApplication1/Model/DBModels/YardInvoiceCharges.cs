using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.DBModels
{
    [Table("YardInvoiceCharges")]
    public class YardInvoiceCharges
    {
        [Key]
        public int YardInvoiceChargeId { get; set; }
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
