using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SezApi.Model.Request
{
    [Table("GatePassDtl")]
    public class GatePassDtl
    {
        [Key]
        public int GatepassDtlId { get; set; }
        public int GatepassId { get; set; }
        public string ContainerNo { get; set; }
        public int? IsReefer { get; set; }
        public string Size { get; set; }
        public string CargoDescription { get; set; }
        public int? CargeType { get; set; }
        public int? NoOfUnits { get; set; }
        public string VehicleNo { get; set; }
        public decimal? Weight { get; set; }
        public string Location { get; set; }
        public string PortOfDispatch { get; set; }
        public decimal? ElwbTareWeight { get; set; }
        public decimal? ElwbCargoWeight { get; set; }
    }
}
