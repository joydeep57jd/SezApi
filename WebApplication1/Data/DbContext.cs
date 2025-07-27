
using Azure;
using Microsoft.EntityFrameworkCore;
using SezApi.Model;
using SezApi.Model.DBModels;
using SezApi.Model.Request;
using SezApi.Model.Response;

namespace SezApi.Data
{
    public class SezApiDbContext : DbContext
    {
        public SezApiDbContext(DbContextOptions<SezApiDbContext> options)
            : base(options)
        {
        }

        public DbSet<test> test { get; set; }
        public DbSet<mststoragecharge> mststoragecharge { get; set; }
        public DbSet<AddEditResponse> AddEditResponse { get; set; }
        public DbSet<GateEntry> GetEntryList { get; set; }
        public DbSet<HTCharges> HTChargesList { get; set; }
       public DbSet<FSCTHCcharges> FSCTHCchargesList { get; set; }
        public DbSet<MstOperation> GetMstOperation { get; set; }
        public DbSet<MstSac> GetMstSac { get; set; }
        public DbSet<MstEntryFee> GetMstEntryFee { get; set; }
        public DbSet<ReeferCharges> GetReeferChargesList { get; set; }
        public DbSet<MovementCharge> GetMovementChargesList { get; set; }
        public DbSet<FumigationCharge> GetFumigationChargesList { get; set; }
        public DbSet<RTRChargeDetails> GetRTRChargesDetailsList { get; set; }
        public DbSet<MstGroundRent> GetMstGroundRent { get; set; }

        public DbSet<MstInsurance> GetMstInsurance { get; set; }

        public DbSet<MstMiscellaneous> GetMstMiscellaneous { get; set; }
        public DbSet<MstRailFreightFees> GetMstRailFreightFees { get; set; }
        public DbSet<MstParty> GetMstParty { get; set; }
        public DbSet<Port> GetPort { get; set; }
        public DbSet<ResponsePort> ResponsePort { get; set; }
        

        public DbSet<MstEximTraderMaster> GetMstEximTraderMaster { get; set; }
        public DbSet<MstCommodity> GetMstCommodity { get; set; }
        public DbSet<State> GetState { get; set; }
        public DbSet<ResponseOBLEntry> ResponseOBLEntry { get; set; }
        public DbSet<GoDown> GetMstGoDown { get; set; }
        public DbSet<Country> GetCountryList { get; set; }
        public DbSet<InvoiceYard> GetYardInvoiceList { get; set; }
        public DbSet<OBLEntry> GetOBLEntry { get; set; }
        public DbSet<OblEntryAdditionalDetails> GetOblEntryAdditionalDetails { get; set; }
        public DbSet<HandlingChargescs> GetHandlinghargesList { get; set; }
        public DbSet<OverTimeCharge> GetOverTimeCharge { get; set; }

        public DbSet<ExaminationCharge> GetExaminationCharge { get; set; }

        public DbSet<ResponseCustomAppraisementApplicationHeader> ResponseCustomAppraisementApplicationHeader { get; set; }

        public DbSet<CustomAppraisementApplicationHeader> CustomAppraisementApplicationHeaderList { get; set; }

        public DbSet<AppraisementDoDetails> GetAppraisementDoDetails { get; set; }

        public DbSet<AppraisementContainerDetails> GetAppraisementContainerDetails { get; set; }
        public DbSet<ResponseImportChargesCalc> ImportChargesCalc { get; set; }
        public DbSet<ResponseStorageChargesCalc> ImportStorageChargesCalc { get; set; }
        public DbSet<ResponseImportInsuaranceCharges> ImportInsuaranceCharges { get; set; }
        public DbSet<ChargesTypes> ListChargesTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<ResponseImportChargesCalc>().HasNoKey();
            modelBuilder.Entity<ResponseAddEdityard>().HasNoKey();
            modelBuilder.Entity<ResponseStorageChargesCalc>().HasNoKey();
            modelBuilder.Entity<ResponseImportInsuaranceCharges>().HasNoKey();
			modelBuilder.Entity<ResponsehandlingCharges>().HasNoKey();
			modelBuilder.Entity<RegisterOfOutwardSupplyReportResponse>().HasNoKey();
		}
		public DbSet<RegisterOfOutwardSupplyReportResponse> RegisterOfOutwardSupplyReportResponse { get; set; }
		
		public DbSet<ResponseCustom> ResponseCustom { get; set; }

        public DbSet<CashReceiptInvDtls> GetCashReceiptInvDtls { get; set; }

        public DbSet<CashReceiptDtl> GetCashReceiptDtl { get; set; }

        public DbSet<CashReceiptHdr> GetCashReceiptHdr { get; set; }

        public DbSet<YardInvoiceCharges> GetYardInvoiceCharges { get; set; }

        public DbSet<FlatImportChargesRow> FlatImportChargesRow { get; set; }

        public DbSet<TransportationCharges> GetTransportationCharges { get; set; }
        public DbSet<StorageChargesGodown> GetStorageChargesGodown { get; set; }

        public DbSet<RentOfficeSpaceCharges> GetRentOfficeSpaceCharges { get; set; }
        public DbSet<RentTableSpaceCharges> GetRentTableSpaceCharges { get; set; }
        public DbSet<GatePass> GatePassHeader { get; set; }
        public DbSet<GatePassDtl> GatePassDetails { get; set; }
        public DbSet<ResponseCustomForExitThroughGate> ResponseCustomForExitThroughGate { get; set; }
        public DbSet<ExitThroughGateHeader> EThroughGateHeader { get; set; }
        public DbSet<ExitThroughGateDetails> EThroughGateDetails { get; set; }
		public DbSet<CCINEntry> CCINEntryDetails { get; set; }
	
        public DbSet<ResponseCustomFor> ResponseCustomFor { get; set; }

        public DbSet<ImpDestuffingEntryHdr> ResponseImpDestuffingEntryHdr { get; set; }
        public DbSet<ImpDestuffingEntryDtl> ResponseImpDestuffingEntryDtl { get; set; }

        public DbSet<ResponseImportTransportChargesCalc> ResponseImportTransportChargesCalc { get; set; }
		public DbSet<ResponsehandlingCharges> ResponsehandlingCharges { get; set; }
		
		public DbSet<LoadContainerRequestHeader> LoadContainerRtHeader { get; set; }
		public DbSet<LoadContainerRequestDetails> LoadContainerRDetails { get; set; }
		public DbSet<ResponseLoadContainerRequest> ResponseLoadContainerRequest { get; set; }
        public DbSet<ImpDeliveryApplicationHdr> RequestImpDeliveryApplicationHdr { get; set; }
        public DbSet<ImpDeliveryApplicationDtl> RequestImpDeliveryApplicationDtl { get; set; }
		public DbSet<ContainerStuffingHeader> ContainerStuffingHeader { get; set; }
		public DbSet<ContainerStuffingDetails> ContainerStuffingDetails { get; set; }
		public DbSet<ResponseContainerStuffing> ResponseContainerStuffing { get; set; }
		public DbSet<ResponseGetContainerlistByGetEntry> ResponseGetContainerlistByGetEntry { get; set; }
		public DbSet<ResponseChargeSummaryByInvoice> ResponseChargeSummaryByInvoice { get; set; }
		public DbSet<mstpackuqc> mstpackuqc { get; set; }
		public DbSet<ResponseGetContainerlistForLoadedContainerRequest> ResponseGetContainerlistForLoadedContainerRequest { get; set; }

		public DbSet<ResponseExportEntryFeeChargesResponse> ResponseExportEntryFeeChargesResponse { get; set; }
		public DbSet<ResponseExportInsuranceChargesResponse> ResponseExportInsuranceChargesResponse { get; set; }

		public DbSet<mstcompany> mstcompany { get; set; }

		public DbSet<GatePassDetailsFlat> GatePassDetailsResponse { get; set; }

        public DbSet<DailyCashBookReportResponse> DailyCashBookReport { get; set; }

        public DbSet<CanceLinvoice> CancelInvoice { get; set; }

        public DbSet<GodownInvoice> GodownInvoice { get; set; }
        public DbSet<GodownInvoiceChargescs> GetGodownInvoiceCharges { get; set; }

        public DbSet<CreditNote> CreditNote { get; set; }

        public DbSet<CreditNoteDetail> creditNoteDetails { get; set; }

        public DbSet<SapResponse> SapResponse { get; set; }

    }
}

