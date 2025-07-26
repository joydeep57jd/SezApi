using System.ComponentModel.DataAnnotations;

namespace SezApi.Model.Response
{
	public class ResponseOblEntryAdditionalDetails
	{
		[Key]
		public int ID { get; set; }

		public int? AddID { get; set; }

		public int? IcesContId { get; set; }

		[StringLength(100)]
		public string OBL_HBL_No { get; set; }

		public DateTime? OBL_HBL_Date { get; set; }

		[StringLength(100)]
		public string SMTP_No { get; set; }

		public DateTime? SMTP_Date { get; set; }

		public string Cargo_Desc { get; set; }

		[StringLength(150)]
		public string Commodity { get; set; }

		[StringLength(50)]
		public string Cargo_Type { get; set; }

		public int? No_of_PKG { get; set; }

		[StringLength(100)]
		public string PKG_Type { get; set; }

		public decimal? GR_WT_Kg { get; set; }

		[StringLength(150)]
		public string Importer_Name { get; set; }

		[StringLength(150)]
		public string IGM_Importer_Name { get; set; }

		public bool? IsProcessed { get; set; }
		public int? OBLEntryId { get; set; }
		public int? CreatedBy { get; set; }
		public DateTime? CreatedOn { get; set; }
		public int? UpdatedBy { get; set; }
		public DateTime? UpdatedOn { get; set; }
		public string ContainerCBTNo { get; set; }
        public string? BOENo { get; set; }
        public DateTime? BOEDate { get; set; }
        public decimal? CIFValue { get; set; }
        public decimal? Duty { get; set; }
    }
}
