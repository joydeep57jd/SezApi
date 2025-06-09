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
        public async Task<ActionResult<List<GetEntry>>> GetAllEntries()
        {

            var response = await _services.GetAllEntries();

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
        public async Task<ActionResult<List<GetEntry>>> GetMstOperation()
        {

            var response = await _services.GetMstOperation();

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


    }
}
