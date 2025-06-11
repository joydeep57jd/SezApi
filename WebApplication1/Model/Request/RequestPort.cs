using System.ComponentModel.DataAnnotations;

namespace SezApi.Model.Request
{
    public class RequestPort
    {
        public int PortId { get; set; } 
        public string PortName { get; set; }
        public string PortAlias { get; set; }
        public bool POD { get; set; }
        public int Country { get; set; }
        public int State { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
