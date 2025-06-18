using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SezApi.Model.Response
{
    [Keyless]
    public class ResponseImportChargesCalc
    {
        
        public decimal TotalEntryValue { get; set; }
        public decimal GrossWeight { get; set; }
        public int NoOfPackets { get; set; }
        public decimal WeightPerPacket { get; set; }
        public decimal RatePerPacket { get; set; }
        public decimal ExtraWeight { get; set; }
        public decimal FeePerPacket { get; set; }
        public decimal ChargablePackets { get; set; }
        public decimal MinimumRate { get; set; }
        public decimal ExtraRatePerKg { get; set; }
        public decimal FinalExamFee { get; set; }
        public decimal CGSTper { get; set; }
        public decimal SGSTper { get; set; }
        public decimal IGSTper { get; set; }
        public decimal EntryCGSTAmount { get; set; }
        public decimal EntrySGSTAmount { get; set; }
        public decimal EntryIGSTAmount { get; set; }
        public decimal TotalEntryAmt { get; set; }
        public decimal EximCGSTAmount { get; set; }
        public decimal EximSGSTAmount { get; set; }
        public decimal EximIGSTAmount { get; set; }
        public decimal TotalExamAmt { get; set; }
    }
}
