using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.DBModels
{
    [Table("State")]
    public class State
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int? CountryId { get; set; }
    }
}
