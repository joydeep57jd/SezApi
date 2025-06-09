using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.DBModels
{
    [Table("mstmiscellaneous")]
    public class MstMiscellaneous
    {
        [Key]
        public int MiscellaneousId { get; set; }
        public int? BranchId { get; set; }
        public decimal? Fumigation { get; set; }
        public decimal? Washing { get; set; }
        public decimal? Reworking { get; set; }
        public decimal? Bagging { get; set; }
        public decimal? Palletizing { get; set; }
        public decimal? Printing { get; set; }
        public decimal? Banking { get; set; }
        public decimal? PhotoCopy { get; set; }
        public decimal? ChequeReturn { get; set; }
        public decimal? Others { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public string SacCode { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string FumigationChargeType { get; set; }
    }
}
