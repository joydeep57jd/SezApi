using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SezApi.Data;
using SezApi.Model.DBModels;
using SezApi.Model.Request;
using SezApi.Model.Response;
using SezApi.Services;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
namespace SezApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SezController : Controller
    {
        private readonly IServices _services;

        public SezController(IServices services)
        {
            _services = services;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpPost("add-test")]
        public async Task<IActionResult> AddTest(test product)
        {
            try
            {
               await _services.AddTest(product);
                return Ok("Product added successfully");
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("AddEditMststorageCharge")]
        public async Task<IActionResult> AddEditMststorageCharge(RequestMststorageCharge request)
        {
            if (request == null)
            {
                return BadRequest("Request data is required.");
            }
            try
            {
                var result =  await _services.AddMststorageCharge(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetMststorageCharge")]
        public async Task<ActionResult<List<mststoragecharge>>> GetMststorageCharge()
        {
            var response = await _services.GetMststorageCharge();

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpPost("AddEditEntry")]
        public async Task<IActionResult> AddEditEntry(RequestGetEntry request)
        {
            if (request == null)
                return BadRequest("Request data is required.");

            try
            {
                var result = await _services.AddEditGetEntry(request);

                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetAllEntries")]
        public async Task<ActionResult<List<GateEntry>>> GetAllEntries(int? page, int? size, string? ContainerNo)
        {

            var response = await _services.GetAllEntries(page, size, ContainerNo);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }
        [HttpPost("AddEditHTCharges")]
        public async Task<IActionResult> AddEditHTCharges(HTChargesRequest request)
        {
            if (request == null)
                return BadRequest("Request data is required.");

            try
            {
                var result = await _services.AddEditHTCharges(request);

                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetAllHTCharges")]
        public async Task<ActionResult<List<HTCharges>>> GetAllHTCharges(int? page, int? size)
        {

            var response = await _services.GetAllHTEntries(page,size);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }
        [HttpPost("AddEditFSCTHCCharges")]
        public async Task<IActionResult> AddEditFSCTHCCharges(RequestFscThcChargeRequest request)
        {
            if (request == null)
                return BadRequest("Request data is required.");

            try
            {
                var result = await _services.AddEditFSCTHCCharges(request);

                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetAllFSCTHCCharges")]
        public async Task<ActionResult<List<FSCTHCcharges>>> GetAllFSCTHCCharges()
        {

            var response = await _services.GetAllFSCTHCCharges();

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpPost("AddEditMstOperation")]
        public async Task<IActionResult> AddEditMstOperation(RequestMstOperation request)
        {
            if (request == null)
            {
                return BadRequest("Request data is required.");
            }
            try
            {
                var result = await _services.AddEditMstOperation(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetMstOperation")]
        public async Task<ActionResult<List<GateEntry>>> GetMstOperation(int? page, int? size)
        {

            var response = await _services.GetMstOperation(page, size);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpPost("AddEditMstSac")]
        public async Task<IActionResult> AddEditMstSac(RequestMstSac request)
        {
            if (request == null)
            {
                return BadRequest("Request data is required.");
            }
            try
            {
                var result = await _services.AddEditMstSac(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetMstSac")]
        public async Task<ActionResult<List<MstSac>>> GetMstSac()
        {

            var response = await _services.GetMstSac();

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpPost("AddEditMstEntryFee")]
        public async Task<IActionResult> AddEditMstEntryFee(RequestMstEntryFee request)
        {
            if (request == null)
            {
                return BadRequest("Request data is required.");
            }
            try
            {
                var result = await _services.AddEditMstEntryFee(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetMstEntryFee")]
        public async Task<IActionResult> GetMstEntryFee()
        {

            var response = await _services.GetMstEntryFee();

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpPost("AddEditReeferCharges")]
        public async Task<IActionResult> AddEditReeferCharges(RequestReeferCharges request)
        {
            if (request == null)
                return BadRequest("Request data is required.");

            try
            {
                var result = await _services.AddEditReeferCharges(request);

                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetAllReeferCharges")]
        public async Task<ActionResult<List<ReeferCharges>>> GetAllReeferCharges()
        {

            var response = await _services.GetAllReeferCharges();

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }
        [HttpPost("AddEditMovementChrg")]
        public async Task<IActionResult> AddEditMovementChrg(RequestMovementCharges request)
        {
            if (request == null)
                return BadRequest("Request data is required.");

            try
            {
                var result = await _services.AddEditMovementChrg(request);

                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetAllMovementCharges")]
        public async Task<ActionResult<List<MovementCharge>>> GetAllMovementCharges()
        {

            var response = await _services.GetAllMovementCharges();

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpPost("AddEditFumigationChrg")]
        public async Task<IActionResult> AddEditFumigationChrg(RequestFumigationCharges request)
        {
            if (request == null)
                return BadRequest("Request data is required.");

            try
            {
                var result = await _services.AddEditFumigationChrg(request);

                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetAllFumigationCharges")]
        public async Task<ActionResult<List<FumigationCharge>>> GetAllFumigationCharges()
        {

            var response = await _services.GetAllFumigationCharges();

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }
        [HttpPost("AddEditRTChargesDtl")]
        public async Task<IActionResult> AddEditRTChargesDtl(RequestRTChargesDtl request)
        {
            if (request == null)
                return BadRequest("Request data is required.");

            try
            {
                var result = await _services.AddEditRTChargesDtl(request);

                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetAllRTChargesDtl")]
        public async Task<ActionResult<List<RTRChargeDetails>>> GetAllRTChargesDtl()
        {

            var response = await _services.GetAllRTChargesDtl();

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }




        [HttpPost("AddEditMstGroundRent")]
        public async Task<IActionResult> AddEditMstGroundRent(RequestMstGroundRent request)
        {
            if (request == null)
            {
                return BadRequest("Request data is required.");
            }
            try
            {
                var result = await _services.AddEditMstGroundRent(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetMstGroundRent")]
        public async Task<ActionResult<List<MstGroundRent>>> GetMstGroundRent()
        {

            var response = await _services.GetMstGroundRent();

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpPost("AddEditMstInsurance")]
        public async Task<IActionResult> AddEditMstInsurance(RequestMstInsurance request)
        {
            if (request == null)
            {
                return BadRequest("Request data is required.");
            }
            try
            {
                var result = await _services.AddEditMstInsurance(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetMstInsurance")]
        public async Task<ActionResult<List<MstInsurance>>> GetMstInsurance(int? page, int? size)
        {

            var response = await _services.GetMstInsurance(page, size);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpPost("AddEditMstMiscellaneouse")]
        public async Task<IActionResult> AddEditMstMiscellaneouse(RequestMstMiscellaneous request)
        {
            if (request == null)
            {
                return BadRequest("Request data is required.");
            }
            try
            {
                var result = await _services.AddEditMstMiscellaneouse(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetMstMiscellaneouse")]
        public async Task<ActionResult<List<MstInsurance>>> GetMstMiscellaneouse()
        {

            var response = await _services.GetMstMiscellaneous();

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpPost("AddEditMstRailFreightFees")]
        public async Task<IActionResult> AddEditMstRailFreightFees(RequestMstRailFreightFees request)
        {
            if (request == null)
            {
                return BadRequest("Request data is required.");
            }
            try
            {
                var result = await _services.AddEditMstRailFreightFees(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetMstRailFreightFees")]
        public async Task<ActionResult<List<MstInsurance>>> GetMstRailFreightFees(int? page, int? size)
        {

            var response = await _services.GetMstRailFreightFees(page, size);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpGet("GetmstParty")]
        public async Task<ActionResult<List<MstInsurance>>> GetmstParty(int? page, int? size, string? partyType)
        {

            var response = await _services.GetMstParty(page, size, partyType);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpGet("GetMstEximTraderMaster")]
        public async Task<ActionResult<List<MstEximTraderMaster>>> GetMstEximTraderMaster(int? page, int? size)
        {

            var response = await _services.GetMstEximTraderMaster(page, size);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpPost("AddEditMstCommodity")]
        public async Task<IActionResult> AddEditMstCommodity(RequestMstCommodity request)
        {
            if (request == null)
            {
                return BadRequest("Request data is required.");
            }
            try
            {
                var result = await _services.AddEditMstCommodity(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetMstCommodity")]
        public async Task<ActionResult<List<MstCommodity>>> GetMstCommodity(int? page, int? size)
        {

            var response = await _services.GetMstCommodity(page, size);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpPost("AddEditPort")]
        public async Task<IActionResult> AddEditPort(RequestPort request)
        {
            if (request == null)
            {
                return BadRequest("Request data is required.");
            }
            try
            {
                var result = await _services.AddEditPort(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetPort")]
        public async Task<ActionResult<List<ResponseAddEditPort>>> GetPort(int? page, int? size)
        {

            var response = await _services.GetPort(page, size);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpGet("GetState")]
        public async Task<ActionResult<List<State>>> GetState(int? id)
        {

            var response = await _services.GetState(id);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }
        [HttpPost("AddEditGoDown")]
        public async Task<IActionResult> AddEditGoDown(RequestGoDown request)
        {
            if (request == null)
            {
                return BadRequest("Request data is required.");
            }
            try
            {
                var result = await _services.AddEditGoDown(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetMstGoDown")]
        public async Task<ActionResult<List<GoDown>>> GetMstGoDown(int? page, int? size)
        {

            var response = await _services.GetMstGoDown(page, size);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpGet("GetCountry")]
        public async Task<ActionResult<List<Country>>> GetCountry(int? page, int? size)
        {

            var response = await _services.GetCountry(page, size);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }
        [HttpPost("AddEditYardInvoice")]
        public async Task<IActionResult> AddEditYardInvoice(RequestYardInvocie request)
        {
            if (request == null)
            {
                return BadRequest("Request data is required.");
            }
            try
            {
                var result = await _services.AddEditYardInvoice(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetYardInvoice")]
        public async Task<ActionResult<List<InvoiceYard>>> GetYardInvoice(int? page, int? size, string? PayeeName, bool? IsLoadContainerInvoice)
        {

            var response = await _services.GetYardInvoice(page, size, PayeeName, IsLoadContainerInvoice);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }



        [HttpPost("AddOblEntry")]
        public async Task<IActionResult> AddOblEntry(RequestOBLEntry request)
        {
            if (request == null)
            {
                return BadRequest("Request data is required.");
            }
            try
            {
                var result = await _services.AddEditOBLEntry(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetOblEntry")]
        public async Task<ActionResult<List<OBLEntry>>> GetOblEntry(int? id, int? page, int? size)
        {

            var response = await _services.GetOblEntry(id,page,size);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpGet("GetOblEntryAdditionalDetails")]
        public async Task<ActionResult<List<OblEntryAdditionalDetails>>> GetOblEntryAdditionalDetails(int? id,int? OBLEntryId)
        {

            var response = await _services.GetOblEntryAdditionalDetails(id, OBLEntryId);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpDelete("RemoveOblEntryAdditionalDetails")]
        public async Task<ActionResult> RemoveOblEntryAdditionalDetails(int id)
        {
            var response = await _services.RemoveOblEntryAdditionalDetails(id);
            return Ok(response);
        }

        [HttpDelete("RemoveEntries")]
        public async Task<ActionResult> RemoveEntries(int id)
        {
            var response = await _services.RemoveEntries(id);
            return Ok(response);
        }

        [HttpPost("AddEditHandlingCharges")]
        public async Task<IActionResult> AddEditHandlingCharges(RequestHandlingCharges request)
        {
            if (request == null)
                return BadRequest("Request data is required.");

            try
            {
                var result = await _services.AddEditHandlingCharges(request);

                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetAllHandlingCharges")]
        public async Task<ActionResult<List<HandlingChargescs>>> GetAllHandlingCharges(int? page, int? size)
        {

            var response = await _services.GetAllHandlingCharges(page, size);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }
        [HttpGet("GetOBLContainerList")]
        public async Task<ActionResult<List<ResponseOBLContauner>>> GetOBLContainerList(int? page, int? size, string? containerNo, string? oblHblNo)
        {

            var response = await _services.GetOBLContainerList( page,  size, containerNo, oblHblNo);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpPost("AddEditOverTimeCharge")]
        public async Task<IActionResult> AddEditOverTimeCharge(RequestOverTimeCharge request)
        {
            if (request == null)
            {
                return BadRequest("Request data is required.");
            }
            try
            {
                var result = await _services.AddEditOverTimeCharge(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetOverTimeCharge")]
        public async Task<ActionResult<List<OverTimeCharge>>> GetOverTimeCharge(int? id, int? page, int? size)
        {

            var response = await _services.GetOverTimeCharge(id, page, size);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpPost("AddEditExaminationCharge")]
        public async Task<IActionResult> AddEditExaminationCharge(RequestExaminationCharge request)
        {
            if (request == null)
            {
                return BadRequest("Request data is required.");
            }
            try
            {
                var result = await _services.AddEditExaminationCharge(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetExaminationCharge")]
        public async Task<ActionResult<List<ExaminationCharge>>> GetExaminationCharge(int? id, int? page, int? size)
        {

            var response = await _services.GetExaminationCharge(id, page, size);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpGet("GetCbtContainerDetailsList")]
        public async Task<ActionResult<List<ResponseCbcContainerList>>> GetCbtContainerDetailsList(int? page, int? size)
        {

            var response = await _services.GetCbtContainerDetailsList(page, size);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpPost("AddEditCustomAppraisementApplicationHeader")]
        public async Task<IActionResult> AddEditCustomAppraisementApplicationHeader(RequestCustomAppraisementApplicationHeader request)
        {
            if (request == null)
            {
                return BadRequest("Request data is required.");
            }
            try
            {
                var result = await _services.AddEditCustomAppraisementApplicationHeader(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetCustomAppraisementApplicationHeader")]
        public async Task<ActionResult<List<CustomAppraisementApplicationHeader>>> GetCustomAppraisementApplicationHeader(int? id, int? page, int? size)
        {

            var response = await _services.GetCustomAppraisementApplicationHeader(id, page, size);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpGet("GetAppraisementDoDetails")]
        public async Task<ActionResult<List<AppraisementDoDetails>>> GetAppraisementDoDetails(int? id, int? page, int? size, int? CustAppId)
        {

            var response = await _services.GetAppraisementDoDetails(id, page, size, CustAppId);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpGet("GetAppraisementContainerDetails")]
        public async Task<ActionResult<List<AppraisementDoDetails>>> GetAppraisementContainerDetails(int? id, int? page, int? size, int? CustAppId)
        {

            var response = await _services.GetAppraisementContainerDetails(id, page, size, CustAppId);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpPost("AddEditPaymentReceipt")]
        public async Task<IActionResult> AddEditPaymentReceipt(RequestCashReceiptCreate request)
        {
            if (request == null)
            {
                return BadRequest("Request data is required.");
            }
            try
            {
                var result = await _services.AddCashReceiptAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetPaymentReceiptHeader")]
        public async Task<ActionResult<List<CashReceiptHdr>>> GetPaymentReceiptHeader(int? id, int? page, int? size)
        {

            var response = await _services.GetPaymentReceiptHeader(id, page, size);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpGet("GetInvoiceDetails")]
        public async Task<ActionResult<List<CashReceiptInvDtls>>> GetInvoiceDetails(int? id, int? page, int? size, int? CashReceiptId)
        {

            var response = await _services.GetInvoiceDetails(id, page, size, CashReceiptId);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpGet("GetPaymentDetails")]
        public async Task<ActionResult<List<CashReceiptDtl>>> GetPaymentDetails(int? id, int? page, int? size, int? CashReceiptId)
        {
            var response = await _services.GetPaymentDetails(id, page, size, CashReceiptId);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpGet("GetOBLEntriesWithDetails")]
        public async Task<IActionResult> GetOBLEntriesWithDetails(int? id = null, string containerNo = null, int? page = null, int? size = null)
        {
            var response = await _services.GetOBLEntriesWithDetails(id, containerNo, page, size);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpPost("GetImportCharges")]
        public async Task<IActionResult> GetImportCharges([FromBody] ImportChargesRequest request)
        {
            var result = await _services.GetImportChargesCalcAsync(
                request.ContainerOBLList,
                request.PartyId,
                request.TypeOfCharge
            );
            return Ok(result);
        }

        [HttpGet("GetAllChargesTypes")]
        public async Task<IActionResult> GetAllChargesTypes()
        {
            var response = await _services.GetAllChargesTypes();

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpGet("GetYardInvoiceCharge")]
        public async Task<IActionResult> GetYardInvoiceCharge(int? id, int? InoviceId, int? page, int? size)
        {
            var response = await _services.GetYardInvoiceCharge(id, InoviceId, page, size);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpGet("GetPaymentReceiptInvoiceDetails")]
        public async Task<IActionResult> GetPaymentReceiptInvoiceDetails(int? id, string? PayeeName,int? payeeId, int? page, int? size)
        {
            var response = await _services.GetPaymentReceiptInvoiceDetails(id, PayeeName, payeeId, page, size);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpGet("GetImportChargesInvoice")]
        public async Task<IActionResult> GetImportChargesInvoice(string? InvoiceNo)
        {
            var response = await _services.GetImportChargesInvoice(InvoiceNo);

            if (response.Data == null )
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpPost("AddEditTransportationCharges")]
        public async Task<IActionResult> AddEditTransportationCharges(RequestTransportationCharges request)
        {
            if (request == null)
            {
                return BadRequest("Request data is required.");
            }
            try
            {
                var result = await _services.AddEditTransportationCharges(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetTransportationCharges")]
        public async Task<IActionResult> GetTransportationCharges(int? id, int? page, int? size)
        {

            var response = await _services.GetTransportationCharges(id, page, size);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }
        [HttpPost("CreateGatePass")]
        public async Task<IActionResult> CreateGatePass([FromBody] GatePassRequest request)
        {
            if (request == null)
            {
                return BadRequest("Request data is required.");
            }

            try
            {
                var result = await _services.CreateGatePassAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost("AddEditStorageChargesGodown")]
        public async Task<IActionResult> AddEditStorageChargesGodown(RequestStorageChargesGodown request)
        {
            if (request == null)
            {
                return BadRequest("Request data is required.");
            }
            try
            {
                var result = await _services.AddEditStorageChargesGodown(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetStorageChargesGodown")]
        public async Task<IActionResult> GetStorageChargesGodown(int? id, int? page, int? size)
        {

            var response = await _services.GetStorageChargesGodown(id, page, size);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpPost("AddEditRequestRentOfficeSpaceCharges")]
        public async Task<IActionResult> AddEditRequestRentOfficeSpaceCharges(RequestRentOfficeSpaceCharges request)
        {
            if (request == null)
            {
                return BadRequest("Request data is required.");
            }
            try
            {
                var result = await _services.AddEditRequestRentOfficeSpaceCharges(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetRequestRentOfficeSpaceCharges")]
        public async Task<IActionResult> GetRequestRentOfficeSpaceCharges(int? id, int? page, int? size)
        {

            var response = await _services.GetRentOfficeSpaceCharges(id, page, size);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpPost("AddEditRentTableSpaceCharges")]
        public async Task<IActionResult> AddEditRentTableSpaceCharges(RequestRentTableSpaceCharges request)
        {
            if (request == null)
            {
                return BadRequest("Request data is required.");
            }
            try
            {
                var result = await _services.AddEditRentTableSpaceCharges(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetRentTableSpaceCharges")]
        public async Task<IActionResult> GetRentTableSpaceCharges(int? id, int? page, int? size)
        {

            var response = await _services.GetRentTableSpaceCharges(id, page, size);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpGet("GetGatePassGateOut")]
        public async Task<IActionResult> GetGatePassGateOut( int? GatePassDtlId)
        {

            var response = await _services.GetGatePassGateOut(GatePassDtlId);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpGet("GetPassHeader")]
        public async Task<IActionResult> GetPassHeader(int? id, int? page, int? size)
        {

            var response = await _services.GetPassHeader(id, page, size);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpGet("GetPassDetails")]
        public async Task<IActionResult> GetPassDetails(int? id,int? gatepassId , int? page, int? size)
        {

            var response = await _services.GetPassDetails(id, gatepassId, page, size);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpPost("CreateExitThroughGate")]
        public async Task<IActionResult> CreateExitThroughGate(RequestExitThroughGate request)
        {
            if (request == null)
            {
                return BadRequest("Request data is required.");
            }

            try
            {
                var result = await _services.CreateExitThroughGate(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetExitThroughHeader")]
        public async Task<IActionResult> GetExitThroughHeader(int? id, int? page, int? size)
        {

            var response = await _services.GetExitThroughHeader(id, page, size);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpGet("GetExitThroughDetails")]
        public async Task<IActionResult> GetExitThroughDetails(int? id, int? page, int? size, int? GateExitHeaderId)
        {

            var response = await _services.GetExitThroughDetails(id, page, size, GateExitHeaderId);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

		[HttpPost("AddEditCCINEntry")]
		public async Task<IActionResult> AddEditCCINEntry(RequestCCINAddEdit request)
		{
			if (request == null)
			{
				return BadRequest("Request data is required.");
			}
			try
			{
				var result = await _services.AddEditCCINEntry(request);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpGet("GetCCINEntry")]
		public async Task<IActionResult> GetCCINEntry(int? id, int? page, int? size)
		{

			var response = await _services.GetCCINEntry(id, page, size);

			if (response.Data == null || !response.Data.Any())
			{
				return NotFound(new { message = "No entries found." });
			}

			return Ok(response);
		}

	

        [HttpPost("AddEditDestuffingEntry")]
        public async Task<IActionResult> AddEditDestuffingEntry(RequestDestuffingEntry request)
        {
            if (request == null)
            {
                return BadRequest("Request data is required.");
            }

            try
            {
                var result = await _services.AddEditDestuffingEntry(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetDestuffingEntryHdr")]
        public async Task<IActionResult> GetDestuffingEntryHdr(int? id, int? page, int? size)
        {

            var response = await _services.GetDestuffingEntryHdr(id, page, size);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpGet("GetDestuffingEntryDtl")]
        public async Task<IActionResult> GetDestuffingEntryDtl(int? id, int? DestuffingEntryId, int? page, int? size)
        {

            var response = await _services.GetDestuffingEntryDtl(id, DestuffingEntryId, page, size);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpGet("GetImportTransportChargesCalc")]
        public async Task<IActionResult> GetImportTransportChargesCalc(string ContainerOBLList, int PartyId)
        {
            try
            {
                var response = await _services.GetImportTransportChargesCalc(ContainerOBLList, PartyId);
                return Ok(response);
            }         

             catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetGetInContainerList")]
        public async Task<IActionResult> GetGetInContainerList()
        {
            try
            {
                var response = await _services.GetGetInContainerList();
                return Ok(response);
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

		[HttpPost("CreateLoadContainerRequest")]
		public async Task<IActionResult> CreateLoadContainerRequest(RequestLoadContainerRequest request)
		{
			if (request == null)
			{
				return BadRequest("Request data is required.");
			}

			try
			{
				var result = await _services.CreateLoadContainerRequest(request);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpGet("GetLoadContainerHeader")]
		public async Task<IActionResult> GetLoadContainerHeader(int? id, int? page, int? size)
		{

			var response = await _services.GetLoadContainerHeader(id, page, size);

			if (response.Data == null || !response.Data.Any())
			{
				return NotFound(new { message = "No entries found." });
			}

			return Ok(response);
		}

		[HttpGet("GetLoadContainerDetails")]
		public async Task<IActionResult> GetLoadContainerDetails(int? id, int? page, int? size, int? LoaderHeaderId)
		{

			var response = await _services.GetLoadContainerDetails(id, page, size, LoaderHeaderId);

			if (response.Data == null || !response.Data.Any())
			{
				return NotFound(new { message = "No entries found." });
			}

			return Ok(response);
		}

        [HttpPost("AddEditDeliveryApplication")]
        public async Task<IActionResult> AddEditDeliveryApplication(RequestImpDeliveryApplication request)
        {
            if (request == null)
            {
                return BadRequest("Request data is required.");
            }

            try
            {
                var result = await _services.AddEditDeliveryApplication(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetImpDeliveryApplicationHdr")]
        public async Task<IActionResult> GetImpDeliveryApplicationHdr(int? id,int? page, int? size)
        {

            var response = await _services.GetImpDeliveryApplicationHdr(id, page, size);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

        [HttpGet("GetImpDeliveryApplicationDtl")]
        public async Task<IActionResult> GetImpDeliveryApplicationDtl(int? id, int? DeliveryId, int? page, int? size)
        {

            var response = await _services.GetImpDeliveryApplicationDtl(id, DeliveryId, page, size);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

		[HttpPost("AddEditContainerStuffing")]
		public async Task<IActionResult> AddEditContainerStuffing(RequestContainerStuffing request)
		{
			if (request == null)
			{
				return BadRequest("Request data is required.");
			}

			try
			{
				var result = await _services.AddEditContainerStuffing(request);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpGet("GetContainerStuffingHdr")]
		public async Task<IActionResult> GetContainerStuffingHdr(int? id, int? page, int? size)
		{

			var response = await _services.GetContainerStuffingHdr(id, page, size);

			if (response.Data == null || !response.Data.Any())
			{
				return NotFound(new { message = "No entries found." });
			}

			return Ok(response);
		}

		[HttpGet("GetContainerStuffingDtl")]
		public async Task<IActionResult> GetContainerStuffingDtl(int? id, int? StuffingId, int? page, int? size)
		{

			var response = await _services.GetContainerStuffingDtl(id, StuffingId, page, size);

			if (response.Data == null || !response.Data.Any())
			{
				return NotFound(new { message = "No entries found." });
			}

			return Ok(response);
		}

	

		[HttpPost("AddGodownInvoice")]
		public async Task<IActionResult> AddGodownInvoice(RequestGodownInvoice request)
		{
			if (request == null)
			{
				return BadRequest("Request data is required.");
			}
			try
			{
				var result = await _services.AddEditGodownInvoice(request);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpPost("GetImportStorageChargesCalc")]
		public async Task<IActionResult> GetImportStorageChargesCalc([FromBody] RequestStorageChargesCalc request)
		{
			var result = await _services.GetImportStorageChargesCalc(
				request.ContainerOBLList,
				request.PartyId,
				request.InvoiceDate
			);
			return Ok(result);
		}

		[HttpPost("GetImportInsuranceChargesCalc")]
        public async Task<IActionResult> GetImportInsuranceChargesCalc([FromBody] RequestInsuaranceCharges request)
        {
            var result = await _services.GetImportInsuranceChargesCalc(
                request.ContainerOBLList,
                request.PartyId,
				request.InvoiceDate
			);
            return Ok(result);
        }

		[HttpGet("GetHandlingChargesCalc")]
		public async Task<IActionResult> GetHandlingChargesCalc(string ContainerOBLList, int PartyId)
		{
			try
			{
				var response = await _services.GetHandlingChargesCalc(ContainerOBLList, PartyId);
				return Ok(response);
			}

			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}
		[HttpGet("GetRegisterOfOutwardSupplyReport")]
		public async Task<IActionResult> GetRegisterOfOutwardSupplyReport(DateTime? FromDate, DateTime? ToDate, string? InvoiceType)
		{
			try
			{
				var response = await _services.GetRegisterOfOutwardSupplyReport(FromDate, ToDate, InvoiceType);
				return Ok(response);
			}

			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

	}
}
