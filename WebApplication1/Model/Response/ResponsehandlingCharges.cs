using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SezApi.Model.Response
{
	public class ResponsehandlingCharges
	{
        public string? ValueType { get; set; }
        public string? ChargeName { get; set; }
		public decimal? TotalValue { get; set; }
		public string? SacCode { get; set; }
		public decimal? CGST { get; set; }
		public decimal? SGST { get; set; }
		public decimal? IGST { get; set; }
		public decimal? CGSTAmount { get; set; }
		public decimal? SGSTAmount { get; set; }
		public decimal? IGSTAmount { get; set; }
		public decimal? TotalAmt { get; set; }
		
	}
}
