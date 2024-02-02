using MechanicalInventory.Models;
using MechanicalInventory.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace MechanicalInventory.Controllers
{
    [Route("api/[controller]")]
    [EnableRateLimiting(policyName: "fixed-rate-limiter")]
    [ApiController]
    public class MahindraController : Ap
    {
        private readonly ILogger<MahindraController> _logger;
        private readonly IMahindraService _mahindraService;
        public MahindraController(ILogger<MahindraController> logger, IMahindraService mahindraService)
        {
            _logger = logger;
            _mahindraService = mahindraService;
        }

        [HttpGet]
        [Route("get-product-list")]
        public async Task<IActionResult> GetMahindraProductsList()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest("Model state is not valid.");
                }
                var result = await _mahindraService.GetProductsList();
                if (result == null)
                {
                    _logger.LogInformation("Mahindra products list is empty.");
                    return Ok("Mahindra products list is empty.");
                }
                else
                {
                    _logger.LogInformation("Successfully got the Mahindra products list.");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in loading Mahindra product list. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("get-product/{id:int}")]
        public async Task<IActionResult> GetMahindraProduct([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest("Model state is not valid.");
                }
                if (await _mahindraService.IsExistsProduct(id))
                {
                    var result = await _mahindraService.GetProduct(id);
                    _logger.LogInformation($"Successfully got the Mahindra product with id : {id}");
                    return Ok(result);
                }
                else
                {
                    _logger.LogInformation("Requested Mahindra product doesn't exists.");
                    return NotFound("Requested Mahindra product doesn't exists.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in getting the Mahindra product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("add-product")]
        public async Task<IActionResult> AddMahindraProduct([FromBody] MahindraProduct mahindraProduct)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest("Model state is not valid.");
                }

                var result = await _mahindraService.AddProduct(mahindraProduct);
                _logger.LogInformation("Successfully added new Mahindra product.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in adding new Mahindra product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("update-product")]
        public async Task<IActionResult> UpdateMahindraProduct([FromBody] MahindraProduct mahindraProduct)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest("Model state is not valid.");
                }
                int productId = mahindraProduct.Id;
                if (await _mahindraService.IsExistsProduct(productId))
                {
                    var result = await _mahindraService.UpdateProduct(mahindraProduct);
                    _logger.LogInformation("Successfully updated existing Mahindra product.");
                    return Ok(result);
                }
                else
                {
                    _logger.LogInformation("Requested Mahindra product doesn't exists.");
                    return NotFound("Requested Mahindra product doesn't exists.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in updating existing Mahindra product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        [Route("delete-product/{id:int}")]
        public async Task<IActionResult> DeleteMahindraProduct([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest("Model state is not valid.");
                }
                if (await _mahindraService.IsExistsProduct(id))
                {
                    var result = await _mahindraService.DeleteProduct(id);
                    _logger.LogInformation($"Successfully deleted existing Mahindra product with id : {id}");
                    return Ok(result);
                }
                else
                {
                    _logger.LogInformation("Requested Mahindra product doesn't exists.");
                    return NotFound("Requested Mahindra product doesn't exists.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in deleting existing Mahindra product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }

        }
    }
}
