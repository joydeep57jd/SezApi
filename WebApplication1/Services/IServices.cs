using Microsoft.AspNetCore.Mvc;
using SezApi.Model.DBModels;
using SezApi.Model.Request;
using SezApi.Model.Response;

namespace SezApi.Services
{
    public interface IServices
    {
        Task<AddEditResponse> AddMststorageCharge(RequestMststorageCharge mststorageCharge);
        Task<Response<List<mststoragecharge>>> GetMststorageCharge();
        Task<AddEditResponse> AddEditGetEntry(RequestGetEntry request);
        Task<Response<List<GateEntry>>> GetAllEntries(int? page, int? size, string? ContainerNo);
        Task<AddEditResponse> AddEditMstOperation(RequestMstOperation request);
        Task<Response<List<MstOperation>>> GetMstOperation(int? page, int? size);
        Task<AddEditResponse> AddEditMstSac(RequestMstSac mststorageCharge);
        Task<Response<List<MstSac>>> GetMstSac();
        Task<AddEditResponse> AddEditMstEntryFee(RequestMstEntryFee request);
        Task<Response<List<MstEntryFee>>> GetMstEntryFee();
        Task<AddEditResponse> AddEditHTCharges(HTChargesRequest request);
        Task<Response<List<HTCharges>>> GetAllHTEntries(int? page, int? size);
        Task<AddEditResponse> AddEditFSCTHCCharges(RequestFscThcChargeRequest request);
        Task<Response<List<FSCTHCcharges>>> GetAllFSCTHCCharges();
        Task<AddEditResponse> AddEditReeferCharges(RequestReeferCharges request);
        Task<Response<List<ReeferCharges>>> GetAllReeferCharges();
        Task<AddEditResponse> AddEditMovementChrg(RequestMovementCharges request);
        Task<Response<List<MovementCharge>>> GetAllMovementCharges();
        Task<AddEditResponse> AddEditFumigationChrg(RequestFumigationCharges request);
        Task<Response<List<FumigationCharge>>> GetAllFumigationCharges();
        Task<AddEditResponse> AddEditRTChargesDtl(RequestRTChargesDtl request);
        Task<Response<List<RTRChargeDetails>>> GetAllRTChargesDtl();
        Task<AddEditResponse> AddEditMstGroundRent(RequestMstGroundRent request);
        Task<Response<List<MstGroundRent>>> GetMstGroundRent();
        Task<AddEditResponse> AddEditMstInsurance(RequestMstInsurance request);
        Task<Response<List<MstInsurance>>> GetMstInsurance(int? page, int? size);
        Task<AddEditResponse> AddEditMstMiscellaneouse(RequestMstMiscellaneous request);
        Task<Response<List<MstMiscellaneous>>> GetMstMiscellaneous();

        Task<AddEditResponse> AddEditMstRailFreightFees(RequestMstRailFreightFees request);
        Task<Response<List<MstRailFreightFees>>> GetMstRailFreightFees(int? page, int? size);
        Task<Response<List<MstParty>>> GetMstParty(int? page, int? size, string? partyType);
        Task<Response<List<MstEximTraderMaster>>> GetMstEximTraderMaster(int? page, int? size);
        Task<AddEditResponse> AddEditMstCommodity(RequestMstCommodity request);
        Task<Response<List<MstCommodity>>> GetMstCommodity(int? page, int? size);
        Task<Response<List<RegisterOfOutwardSupplyReportResponse>>> GetRegisterOfOutwardSupplyReport(DateTime? FromDate, DateTime? ToDate, string InvoiceType);

        Task<ResponsePort> AddEditPort(RequestPort request);
        Task<Response<List<ResponseAddEditPort>>> GetPort(int? page, int? size);
        Task<Response<List<State>>> GetState(int? id);
        Task<AddEditResponse> AddEditGoDown(RequestGoDown request);
        Task<Response<List<GoDown>>> GetMstGoDown(int? page, int? size);
        Task<Response<List<Country>>> GetCountry(int? page, int? size);
        Task<AddEditResponse> AddEditYardInvoice(RequestYardInvocie request);
        Task<Response<List<InvoiceYard>>> GetYardInvoice(int? page, int? size, string? PayeeName, bool? IsLoadContainerInvoice, bool? isCancelled, bool? forGetpass);
        Task<AddEditResponse> AddEditOBLEntry(RequestOBLEntry request);
        Task<Response<List<OBLEntry>>> GetOblEntry(int? id, int? page, int? size);
        Task<Response<List<ResponseOblEntryAdditionalDetails>>> GetOblEntryAdditionalDetails(int? id, int? OBLEntryId);
        Task<AddEditResponse> RemoveOblEntryAdditionalDetails(int OBLEntryId);
        Task<AddEditResponse> RemoveEntries(int id);
        Task<AddEditResponse> AddEditHandlingCharges(RequestHandlingCharges request);
         Task<Response<List<HandlingChargescs>>> GetAllHandlingCharges(int? page, int? size);
        Task<Response<List<ResponseOBLContauner>>> GetOBLContainerList(int? page, int? size, string? containerNo, string? oblHblNo, string? AppNo);
        Task<AddEditResponse> AddEditOverTimeCharge(RequestOverTimeCharge request);
        Task<Response<List<OverTimeCharge>>> GetOverTimeCharge(int? id, int? page, int? size);
        Task<AddEditResponse> AddEditExaminationCharge(RequestExaminationCharge request);
        Task<Response<List<ExaminationCharge>>> GetExaminationCharge(int? id, int? page, int? size);
        Task<Response<List<ResponseCbcContainerList>>> GetCbtContainerDetailsList(int? page, int? size);
        Task<AddEditResponse> AddEditCustomAppraisementApplicationHeader(RequestCustomAppraisementApplicationHeader request);
        Task<Response<ResponsehandlingCharges>> GetHandlingChargesCalc(string containerLoadConReqList, int partyId);

        Task<Response<List<ResponseCustomerHeaderForList>>> GetCustomAppraisementApplicationHeader(int? id, int? page, int? size, bool? isInvoiceCheck);
        Task<Response<List<AppraisementDoDetails>>> GetAppraisementDoDetails(int? id, int? page, int? size, int? CustAppId);
        Task<Response<List<AppraisementContainerDetails>>> GetAppraisementContainerDetails(int? id, int? page, int? size, int? CustAppId);
        Task<Response<List<ResponseOBLEntryWithDetailsDto>>> GetOBLEntriesWithDetails(int? id = null, string containerNo = null, int? page = null, int? size = null);
        Task<Response<List<ResponseImportChargesCalc>>> GetImportChargesCalcAsync(string containerOBLList, int partyId, int typeOfCharge,bool isYardInvoice);
        Task<Response<List<ChargesTypes>>> GetAllChargesTypes();
        Task<AddEditResponse> AddCashReceiptAsync(RequestCashReceiptCreate request);
        Task<Response<List<CashReceiptInvDtls>>> GetInvoiceDetails(int? id, int? page, int? size, int? CashReceiptId, bool? ForGatePass);
        Task<Response<List<CashReceiptDtl>>> GetPaymentDetails(int? id, int? page, int? size, int? CashReceiptId);
        Task<Response<List<CashReceiptHdr>>> GetPaymentReceiptHeader(int? id, int? page, int? size);
        Task<Response<List<YardInvoiceCharges>>> GetYardInvoiceCharge(int? id, int? InoviceId, int? page, int? size);
        Task<Response<List<ResponseYardInvoiceFlat>>> GetPaymentReceiptInvoiceDetails(int? id, string? PayeeName, int? payeeId, int? page, int? size);
        Task<Response<ResponseImportChargesInvoice>> GetImportChargesInvoice(string? InvoiceNo);
        Task<AddEditResponse> AddEditTransportationCharges(RequestTransportationCharges request);

        Task<Response<List<TransportationCharges>>> GetTransportationCharges(int? id, int? page, int? size);

        Task<AddEditResponse> AddEditStorageChargesGodown(RequestStorageChargesGodown request);

        Task<Response<List<StorageChargesGodown>>> GetStorageChargesGodown(int? id, int? page, int? size);
        Task<AddEditResponse> AddEditRequestRentOfficeSpaceCharges(RequestRentOfficeSpaceCharges request);
        Task<Response<List<RentOfficeSpaceCharges>>> GetRentOfficeSpaceCharges(int? id, int? page, int? size);

        Task<AddEditResponse> AddEditRentTableSpaceCharges(RequestRentTableSpaceCharges request);

        Task<Response<List<RentTableSpaceCharges>>> GetRentTableSpaceCharges(int? id, int? page, int? size);
        Task<AddEditResponse> CreateGatePassAsync(GatePassRequest request);
        Task<Response<List<ResponseGatePassGateOut>>> GetGatePassGateOut(int? GatePassDtlId);
        Task<Response<List<ResponseGatePass>>> GetPassHeader(int? id, int? page, int? size, bool? ForGateExit);
        Task<Response<List<GatePassDtl>>> GetPassDetails(int? id, int? gatepassId, int? page, int? size);
        Task<Response<List<ExitThroughGateHeader>>> GetExitThroughHeader(int? id, int? page, int? size);
        Task<AddEditResponse> CreateExitThroughGate(RequestExitThroughGate request);
        Task<Response<List<ExitThroughGateDetails>>> GetExitThroughDetails(int? id, int? page, int? size, int? GateExitHeaderId);
        Task<AddEditResponse> AddEditCCINEntry(RequestCCINAddEdit request);
        Task<Response<List<CCINEntry>>> GetCCINEntry(int? id, int? page, int? size, string? SBNo, DateTime? SBDate);
        Task<Response<List<LoadContainerRequestHeader>>> GetLoadContainerHeader(int? id, int? page, int? size);
        Task<Response<List<LoadContainerRequestDetails>>> GetLoadContainerDetails(int? id, int? page, int? size, int? LoaderHeaderId);
        Task<AddEditResponse> AddEditDestuffingEntry(RequestDestuffingEntry request);
        Task<Response<List<ImpDestuffingEntryHdr>>> GetDestuffingEntryHdr(int? id, int? page, int? size);
        Task<Response<List<ImpDestuffingEntryDtl>>> GetDestuffingEntryDtl(int? id, int? DestuffingEntryId, int? page, int? size);
        Task<ResponseImportTransportChargesCalc> GetImportTransportChargesCalc(string ContainerOBLList, int PartyId, bool IsYardInvoice);
        Task<AddEditResponse> CreateLoadContainerRequest(RequestLoadContainerRequest request);
        Task<Response<List<ResponseGetinContainer>>> GetGetInContainerList(string? OperationName, string? DeliveryType);
        Task<AddEditResponse> AddEditDeliveryApplication(RequestImpDeliveryApplication request);
        Task<Response<List<ImpDeliveryApplicationHdr>>> GetImpDeliveryApplicationHdr(int? id, int? page, int? size, bool? isInvoiceCheck);
        Task<Response<List<ResponseImpDeliveryApplicationDtl>>> GetImpDeliveryApplicationDtl(int? id, int? DeliveryId, int? page, int? size);
        Task<AddEditResponse> AddEditContainerStuffing(RequestContainerStuffing request);
        Task<Response<List<ContainerStuffingHeader>>> GetContainerStuffingHdr(int? id, int? page, int? size, bool? isInvoice);
        Task<Response<List<ContainerStuffingDetails>>> GetContainerStuffingDtl(int? id, int? StuffingId, int? page, int? size);
        Task<Response<List<ResponseStorageChargesCalc>>> GetImportStorageChargesCalc(string containerOBLList, int partyId, DateTime InvoiceDate);
        Task<Response<List<ResponseImportInsuaranceCharges>>> GetImportInsuranceChargesCalc(string containerOBLList, int partyId, bool isYardInvoice);
        Task<AddEditResponse> AddEditGodownInvoice(RequestGodownInvoice request);
        Task<Response<List<RegisterOfOutwardSupplyReportResponse>>> GetRegisterOfOutwardSupplyReportInvoice(DateTime? FromDate, DateTime? ToDate, string InvoiceType);
        Task<Response<List<RegisterOfOutwardSupplyReportResponse>>> GetRegisterOfOutwardSupplyReportCancel(DateTime? FromDate, DateTime? ToDate, string InvoiceType);
        Task<Response<List<ResponseChargeSummaryByInvoice>>> GetChargeSummaryByInvoiceResponse();
        Task<Response<List<ResponseGetContainerlistByGetEntry>>> GetContainerlistByGetEntry();
		Task<Response<List<ResponseGetContainerlistForLoadedContainerRequest>>> GetContainerlistForLoadedContainerRequest();
		Task<Response<List<ResponseGetCLandRno>>> GetCLandRNoForLoadContainerInvoice(string? RequestNo);
        Task<Response<List<ResponseGetContainerlistByGetEntry>>> GetContainerlistByOBLEntry();
        Task<Response<List<CCINEntry>>> GetCCINEntryBySBNo(int? id, int? page, int? size, string? SBNo);
        Task<Response<List<mstpackuqc>>> GetPackUQC(int? id, int? page, int? size);
		Task<Response<List<ResponseExportEntryFeeChargesResponse>>> GetExportEntryFeeChargesResponse(string ContainerList, int PartyId,bool isLoadContainerInvoice, int TypeOfCharge);
		Task<Response<List<ResponseExportInsuranceChargesResponse>>> GetExportInsuranceChargesCalc(string ContainerList, int PartyId, DateTime InvoiceDate, bool isLoadContainerInvoice);

		Task<Response<List<mstcompany>>> GetComanyDetails(int? id);

		Task<Response<GatePassDetailsStructured>> GetGatePassDetailsStructured(string invoiceNo);

        Task<Response<List<DailyCashBookReportResponse>>> GetDailyCashBookReport(DateTime? fromDate, DateTime? toDate);

        Task<AddEditResponse> CancelInvoiceAsync(RequestCanceLinvoice reqInv);

        Task<Response<List<ResponseCanceLinvoice>>> GetCancelInvoiceAsync(int? id, int? page, int? size, string? InvoiceNo);
        Task<Response<List<GodownInvoice>>> GetGodownInvoice(int? id, int? page, int? size, bool? isImport);
        Task<Response<ResponseImportChargesInvoice>> GetGodownChargesReport(string? InvoiceNo);
        Task<Response<ResponsehandlingCharges>> GetExGodownHandlingChargesCalc(string ContainerShLineList, int partyId);
        Task<Response<ResponseInvoiceByPayee>> GetPaymentInvoiceDetailsByPayee(string? PayeeName, int? payeeId);

        Task<Response<ResponseChargesRateSac>> GetChargesRateBySacCode(string? chargeType, string? SacCode, string isImport = "import", bool ishigh = false);
        Task<AddEditResponse> CreateCreditNoteAsync(RequestCreditNote request);

        Task<Response<List<CreditNote>>> GetCreditNoteList(int? id, int? page, int? size, string? creditNoteNo);
        Task<Response<List<CreditNoteDetail>>> GetCreditNoteDetailList(int? CreditNoteDetailId, int? creditNoteId, int? page, int? size);
        Task<ResponseImportTransportChargesCalc> GetExportTransportChargesCalc(string ContainerList, int PartyId, bool isLoadContainerInvoice);

        Task<AddEditResponse> TestCWCapi(GetInvoiceDtlforSAPRequest request, int invId);
    }
}
