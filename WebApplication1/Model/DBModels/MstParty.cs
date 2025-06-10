using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.DBModels
{
    [Table("mstParty")]
    public class MstParty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PartyId { get; set; }

        [Required]
        [StringLength(255)]
        public string PartyName { get; set; }
    }
}
