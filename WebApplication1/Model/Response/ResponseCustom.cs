using System.ComponentModel.DataAnnotations;

namespace SezApi.Model.Response
{
    public class ResponseCustom
    {
        [Key]
        public string? Response { get; set; }  // "OK" or "NOT OK"
        public int? Id { get; set; }
        public string? ErrorMessage { get; set; }  // Optional if included in SP
    }

    public class ResponseCustomForExitThroughGate
    {
        [Key]
        public string? Response { get; set; }  // "OK" or "NOT OK"
        public int? Id { get; set; }
    }
    public class ResponseCustomFor
    {
        [Key]
        public string? Response { get; set; }  // "OK" or "NOT OK"
        public int? Id { get; set; }
    }
}
