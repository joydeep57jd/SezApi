using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.Response
{
    [Table("mstChargesType")]
    public class ChargesTypes
    {
        [Key]
        public int ChargeId { get; set; }
        public string ChargeCode { get; set; }
        public string ChargeName { get; set; }
    }
}
