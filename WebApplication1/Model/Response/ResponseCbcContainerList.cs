using System.ComponentModel.DataAnnotations;

namespace SezApi.Model.Response
{
    public class ResponseCbcContainerList
    {
        [Key]

        public string ContainerCBTNo { get; set; }
        public string Cargo_Type { get; set; }
        public int? No_of_PKG { get; set; }
        public string OBL_HBL_No { get; set; }
        public decimal? GR_WT_Kg { get; set; }
    }
}
