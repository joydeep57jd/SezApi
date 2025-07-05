using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.Request
{
    public class RequestMstInsurance
    {
        public int InsuranceId { get; set; }
		[Column(TypeName = "decimal(10,3)")]
		public decimal? Rate { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public int? BranchId { get; set; }
        public int? SacCodeId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
