using System.ComponentModel.DataAnnotations;

namespace SezApi.Model.DBModels
{
    public class test
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
    }
}
