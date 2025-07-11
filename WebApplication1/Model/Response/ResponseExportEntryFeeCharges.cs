namespace SezApi.Model.Response
{
	public class ResponseExportEntryFeeCharges
	{

		public decimal TotalEntryValue { get; set; }
		public string? EntrySacCode { get; set; }
		public decimal CGSTper { get; set; }
		public decimal SGSTper { get; set; }
		public decimal IGSTper { get; set; }
		public decimal EntryCGSTAmount { get; set; }
		public decimal EntrySGSTAmount { get; set; }
		public decimal EntryIGSTAmount { get; set; }
		public decimal TotalEntryAmt { get; set; }
		
	}
}
