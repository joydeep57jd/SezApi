using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using SezApi.Data;
using SezApi.Model.DBModels;
using SezApi.Model.Request;
using SezApi.Model.Response;
using SezApi.Services;
using System.Diagnostics.Eventing.Reader;
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
        public async Task<ActionResult<List<GateEntry>>> GetAllEntries(int? page, int? size)
        {

            var response = await _services.GetAllEntries(page, size);

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
        public async Task<ActionResult<List<InvoiceYard>>> GetYardInvoice(int? page, int? size)
        {

            var response = await _services.GetYardInvoice(page, size);

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
        public async Task<ActionResult<List<ResponseOBLContauner>>> GetOBLContainerList(int? page, int? size)
        {

            var response = await _services.GetOBLContainerList( page,  size);

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
    }
}
