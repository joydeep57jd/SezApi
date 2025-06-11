using System.ComponentModel.DataAnnotations;

namespace SezApi.Model.Request
{
    public class RequestMstCommodity
    {
        public int CommodityId { get; set; }
        public string? CommodityName { get; set; }
        public string? CommodityType { get; set; }
        public string? Alias { get; set; }
        public bool IsTaxExempted { get; set; }
        public bool IsFumigationChemical { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
