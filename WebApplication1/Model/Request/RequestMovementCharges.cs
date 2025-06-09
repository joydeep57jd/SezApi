namespace SezApi.Model.Request
{
    public class RequestMovementCharges
    {
        public int MovementChargeId { get; set; } = 0;
        public string MovementBy { get; set; }
        public string Origin { get; set; }
        public string MovementVia { get; set; }
        public string Size { get; set; }
        public int CargoType { get; set; } = 0;
        public decimal? MovementRate { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public int CreatedBy { get; set; } = 0;
        public int? ModifiedBy { get; set; }
    }
}
