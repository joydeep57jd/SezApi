using Microsoft.EntityFrameworkCore;
namespace SezApi.Model.Response
{
    [Keyless]
    public class ResponseStorageChargesCalc
    {
        public decimal TotalStorageChargevalue { get; set; }
        public int SacCode { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public decimal IGST { get; set; }
        public decimal CGSTAmount { get; set; }
        public decimal SGSTAmount { get; set; }
        public decimal IGSTAmount { get; set; }
        public decimal TotalAmt { get; set; }
     
    }
}
