using System.ComponentModel.DataAnnotations;

namespace SezApi.Model.Response
{
    public class ResponseCustomAppraisementApplicationHeader
    {
        [Key]
        public string Response { get; set; }
        public int? Id { get; set; }
    }
}
