using System.ComponentModel.DataAnnotations;

namespace SezApi.Model.Request
{
    public class RequestExaminationCharge
    {
        [Key]
        public int ExaminationChargeId { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public int? SACCodeId { get; set; }

        public string? ExaminationFor { get; set; }

        public decimal? ExaminationPercent { get; set; }

        public decimal? RatePerPacket { get; set; }

        public decimal? MinimumCharges { get; set; }

        public decimal? WeightForAdditionalCharges { get; set; }

        public decimal? RateForAdditionalCharges { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string? ModifiedBy { get; set; }
    }
}
