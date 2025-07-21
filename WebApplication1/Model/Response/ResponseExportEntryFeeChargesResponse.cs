using System.ComponentModel.DataAnnotations;

namespace SezApi.Model.Response
{
	public class ResponseExportEntryFeeChargesResponse
	{
		[Key]
		public decimal? TotalEntryValue { get; set; }         
		public string? EntrySacCode { get; set; }            
		public decimal? CGSTper { get; set; }               
		public decimal? SGSTper { get; set; }                 
		public decimal? IGSTper { get; set; }                
		public decimal? EntryCGSTAmount { get; set; }         
		public decimal? EntrySGSTAmount { get; set; }        
		public decimal? EntryIGSTAmount { get; set; }     
		public decimal? TotalEntryAmt { get; set; }
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
