using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using SezApi.Data;
using SezApi.Model.DBModels;
using SezApi.Model.Request;
using SezApi.Model.Response;
using SezApi.Services;
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

        [HttpGet("GetAllHTEntries")]
        public async Task<ActionResult<List<HTCharges>>> GetAllHTEntries()
        {

            var response = await _services.GetAllHTEntries();

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
        public async Task<ActionResult<List<MstSac>>> GetMstEntryFee()
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
        public async Task<ActionResult<List<MstInsurance>>> GetMstInsurance()
        {

            var response = await _services.GetMstInsurance();

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
        public async Task<ActionResult<List<MstInsurance>>> GetmstParty(int? page, int? size)
        {

            var response = await _services.GetMstParty(page, size);

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
        public async Task<ActionResult<List<Port>>> GetPort(int? page, int? size)
        {

            var response = await _services.GetPort(page, size);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = "No entries found." });
            }

            return Ok(response);
        }

    }
}
