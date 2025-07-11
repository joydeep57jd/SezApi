using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.DBModels
{
	[Table("mstcompany")]
	public class mstcompany
	{
		[Key]
		public int CompanyId { get; set; }
		public string? ROAddress { get; set; }
		public string? CompanyName { get; set; }
		public string? CompanyShortName { get; set; }
		public string? CompanyAddress { get; set; }
		public string? PhoneNo { get; set; }
		public string? FaxNumber { get; set; }
		public string? EmailAddress { get; set; }
		public int? StateId { get; set; }
		public string? StateCode { get; set; }
		public int? CityId { get; set; }
		public string? CFSFormat { get; set; }
		public string? GstIn { get; set; }
		public string? Pan { get; set; }
		public int? BranchId { get; set; }
		public string? InvoiceStateCode { get; set; }
		public string? InvoiceCfsCode { get; set; }
		public string? BillOfSupplyCfsCode { get; set; }
		public string? CRNoteCfsCode { get; set; }
		public string? DRNoteCfsCode { get; set; }
		public string? AddMoneyToPdCfsCode { get; set; }
		public int? AuctionEffectiveDays { get; set; }
		public int? DueDaysAftrAucNtc { get; set; }
		public int? AucNtcDaysAftrLanding { get; set; }
		public int? FreeDaysAfterAuction { get; set; }
		public string? AuctionNoticeNoPrefix { get; set; }
		public string? AuctionNoticeCC { get; set; }
		public string? AuctionNoticeDocPrefix { get; set; }
		public int? CreatedBy { get; set; }
		public DateTime? CreatedOn { get; set; }
		public string? BidNumberPrefix { get; set; }
		public string? LocationUrl { get; set; }
		public string? ContactAddress { get; set; }
		public string? ContactPhone { get; set; }
		public string? BranchType { get; set; }
		public string? BranchName { get; set; }
		public decimal? Version { get; set; }
		public string? Effectlogofile { get; set; }
		public string? ClientID { get; set; }
		public string? ClientSecret { get; set; }
		public string? UserName { get; set; }
		public string? Password { get; set; }
		public string? PinCode { get; set; }
		public string? PortofReporting { get; set; }
		public string? ReportingLocationCode { get; set; }
		public string? ReportingLocationName { get; set; }
		public string? AuthorizedPersonPAN { get; set; }
		public string? SCMTREnvironment { get; set; }
		public string? SenderId { get; set; }
		public string? ReceiverId { get; set; }
		public string? VersionNo { get; set; }
		public string? SCMTRUserId { get; set; }
		public string? Location { get; set; }
		public string? DSCPASSWORD { get; set; }
		public string? Ver { get; set; }
		public int? Mode { get; set; }
		public int? OrgId { get; set; }
		public string? Tid { get; set; }
		public string? Pa { get; set; }
		public int? Mc { get; set; }
		public string? Mid { get; set; }
		public string? Msid { get; set; }
		public string? Mtid { get; set; }
		public string? QrMedium { get; set; }
		public int? QRexpireDays { get; set; }
		public string? Tier { get; set; }
		public string? Pn { get; set; }
		public string? ARVersion { get; set; }
		public string? Ccavenuemid { get; set; }
		public string? CcavenueCancelURL { get; set; }
		public string? CcavenueRedirectURL { get; set; }
		public string? MKey { get; set; }
		public string? BqrAcoountId { get; set; }
		public string? InvoiceSLACode { get; set; }
		public string? ProfitCenter { get; set; }
		public string? WarehouseCode { get; set; }
		public string? BusinessPlace { get; set; }
		public string? SectionCode { get; set; }
	}
}