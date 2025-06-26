using System.ComponentModel.DataAnnotations;

namespace SezApi.Model.Response
{
    public class ResponseImportTransportChargesCalc
    {
        [Key]
        public int NoOfPackets { get; set; }
        public string? ChargeName_HV { get; set; }
        public decimal? TotalHighValue { get; set; }
        public string? SacCode_HV { get; set; }
        public decimal? CGST_HV { get; set; }
        public decimal? SGST_HV { get; set; }
        public decimal? IGST_HV { get; set; }
        public decimal? HighValueCGSTAmount { get; set; }
        public decimal? HighValueSGSTAmount { get; set; }
        public decimal? HighValueIGSTAmount { get; set; }
        public decimal? TotalAmt_HV { get; set; }
        public string? ChargeName_LV { get; set; }
        public decimal? TotLowValue { get; set; }
        public string? SacCode_LV { get; set; }
        public decimal? CGST_LV { get; set; }
        public decimal? SGST_LV { get; set; }
        public decimal? IGST_LV { get; set; }
        public decimal? LowValueCGSTAmount { get; set; }
        public decimal? LowValueSGSTAmount { get; set; }
        public decimal? LowValueIGSTAmount { get; set; }
        public decimal? TotalAmt_LV { get; set; }
    }
}
