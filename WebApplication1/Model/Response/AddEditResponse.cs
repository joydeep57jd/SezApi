using SezApi.Model.DBModels;
using System.ComponentModel.DataAnnotations;

namespace SezApi.Model.Response
{
    public class AddEditResponse
    {
        [Key]
        public string Response { get; set; }

    }
}
