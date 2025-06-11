using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.DBModels
{
    [Table("mstport")]
    public class Port
    {
        [Key]
        public int PortId { get; set; }  
        public string PortName { get; set; }
        public string PortAlias { get; set; }
        public bool POD { get; set; }
        public int? Country { get; set; }
        public int? State { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
