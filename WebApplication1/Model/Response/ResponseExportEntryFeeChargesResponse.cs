using System.ComponentModel.DataAnnotations;

namespace SezApi.Model.Response
{
	public class ResponseExportEntryFeeChargesResponse
	{
		[Key]
        public decimal GrossWeight { get; set; }
        public int NoOfPackets { get; set; }
        public decimal WeightPerPacket { get; set; }
        public decimal RatePerPacket { get; set; }
        public decimal ExtraWeight { get; set; }
        public decimal FeePerPacket { get; set; }
        public decimal ChargablePackets { get; set; }
        public decimal MinimumRate { get; set; }
        public decimal ExtraRatePerKg { get; set; }
        public decimal TotalEntryValue { get; set; }
        public string? EntrySacCode { get; set; }
        public decimal CGSTper { get; set; }
        public decimal SGSTper { get; set; }
        public decimal IGSTper { get; set; }
        public decimal EntryCGSTAmount { get; set; }
        public decimal EntrySGSTAmount { get; set; }
        public decimal EntryIGSTAmount { get; set; }
        public decimal TotalEntryAmt { get; set; }
        public decimal TotalExamValue { get; set; }
        public string? ExaminationSacCode { get; set; }
        public decimal CGSTperExam { get; set; }
        public decimal SGSTperExam { get; set; }
        public decimal IGSTperExam { get; set; }
        public decimal ExamCGSTAmount { get; set; }
        public decimal ExamSGSTAmount { get; set; }
        public decimal ExamIGSTAmount { get; set; }

        public decimal TotalExamAmt { get; set; }
    }

	public class ResponseExportInsuranceChargesResponse
	{
		[Key]
		public decimal TotalInsuranceValue { get; set; }
		public string SacCode { get; set; }
		public decimal CGST { get; set; }
		public decimal SGST { get; set; }
		public decimal IGST { get; set; }
		public decimal CGSTAmount { get; set; }
		public decimal SGSTAmount { get; set; }
		public decimal IGSTAmount { get; set; }
		public decimal TotalAmt { get; set; }
	}

}
