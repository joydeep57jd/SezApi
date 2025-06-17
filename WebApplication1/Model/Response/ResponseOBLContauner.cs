using System.ComponentModel.DataAnnotations;

namespace SezApi.Model.Response
{
    public class ResponseOBLContauner
    {
        [Key]
        public string ContainerCBTNo { get; set; }
        public string OBL_HBL_No { get; set; }

        public string? ICDNo { get; set; }
        public string? Size { get; set; }
        public bool? Reefer { get; set; }
        public string? CargoType { get; set; }
        public int? NoOfPackage { get; set; }
        public decimal? GrWt { get; set; }
    }
}
