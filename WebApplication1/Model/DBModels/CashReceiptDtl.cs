using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SezApi.Model.DBModels
{
    [Table("cashreceiptdtl")]
    public class CashReceiptDtl
    {
        [Key]
        public int CashReceiptDtlId { get; set; }
        public int CashReceipthdrId { get; set; }
        public string PayMode { get; set; }
        public string InstrumentNo { get; set; }
        public string DraweeBank { get; set; }
        public string Date { get; set; }
        public decimal? Amount { get; set; }
        public string IsChqCancelled { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
