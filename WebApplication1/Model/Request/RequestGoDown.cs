using System.ComponentModel.DataAnnotations;

namespace SezApi.Model.Request
{
    public class RequestGoDown
    {
        public int GodownId { get; set; } = 0;
        public string GodownName { get; set; }
        public string LocationAlias { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
