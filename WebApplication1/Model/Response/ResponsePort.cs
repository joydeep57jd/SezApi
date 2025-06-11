using System.ComponentModel.DataAnnotations;

namespace SezApi.Model.Response
{
    public class ResponsePort
    {
        [Key]
        public int PortId { get; set; }
    }
}
