using System.ComponentModel.DataAnnotations;

namespace SezApi.Model.Request
{
    public class RequestMstInsurance
    {
        public int InsuranceId { get; set; }
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
