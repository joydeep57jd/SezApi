using System.ComponentModel.DataAnnotations;

namespace SezApi.Model.Response
{
    public class ResponseOBLContauner
    {
        [Key]
        public string ContainerCBTNo { get; set; }
        public string OBL_HBL_No { get; set; }
    }
}
