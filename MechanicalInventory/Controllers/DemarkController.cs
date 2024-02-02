using MechanicalInventory.Models;
using MechanicalInventory.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace MechanicalInventory.Controllers
{
    [Route("api/[controller]")]
    [EnableRateLimiting(policyName: "fixed-rate-limiter")]
    [ApiController]
    public class DemarkController : ControllerBase
    {
        private readonly ILogger<DemarkController> _logger;
        private readonly IDemarkService _demarkService;
        public DemarkController(ILogger<DemarkController> logger, IDemarkService demarkService)
        {
            _logger = logger;
            _demarkService = demarkService;
        }

        [HttpGet]
        [Route("get-product-list")]
        public async Task<IActionResult> GetDemarkProductsList()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest("Model state is not valid.");
                }
                var result = await _demarkService.GetProductsList();
                if (result == null)
                {
                    _logger.LogInformation("Demark products list is empty.");
                    return Ok("Demark products list is empty.");
                }
                else
                {
                    _logger.LogInformation("Successfully got the Demark products list.");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in loading Demark product list. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("get-product/{id:int}")]
        public async Task<IActionResult> GetDemarkProduct([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest("Model state is not valid.");
                }
                if (await _demarkService.IsExistsProduct(id))
                {
                    var result = await _demarkService.GetProduct(id);
                    _logger.LogInformation($"Successfully got the Demark product with id : {id}");
                    return Ok(result);
                }
                else
                {
                    _logger.LogInformation("Requested Demark product doesn't exists.");
                    return NotFound("Requested Demark product doesn't exists.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in getting the Demark product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("add-product")]
        public async Task<IActionResult> AddDemarkProduct([FromBody] DemarkProduct demarkProduct)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest("Model state is not valid.");
                }

                var result = await _demarkService.AddProduct(demarkProduct);
                _logger.LogInformation("Successfully added new Demark product.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in adding new Demark product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("update-product")]
        public async Task<IActionResult> UpdateDemarkProduct([FromBody] DemarkProduct demarkProduct)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest("Model state is not valid.");
                }
                int productId = demarkProduct.Id;
                if (await _demarkService.IsExistsProduct(productId))
                {
                    var result = await _demarkService.UpdateProduct(demarkProduct);
                    _logger.LogInformation("Successfully updated existing Demark product.");
                    return Ok(result);
                }
                else
                {
                    _logger.LogInformation("Requested Demark product doesn't exists.");
                    return NotFound("Requested Demark product doesn't exists.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in updating existing Demark product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        [Route("delete-product/{id:int}")]
        public async Task<IActionResult> DeleteDemarkProduct([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest("Model state is not valid.");
                }
                if (await _demarkService.IsExistsProduct(id))
                {
                    var result = await _demarkService.DeleteProduct(id);
                    _logger.LogInformation($"Successfully deleted existing Demark product with id : {id}");
                    return Ok(result);
                }
                else
                {
                    _logger.LogInformation("Requested Demark product doesn't exists.");
                    return NotFound("Requested Demark product doesn't exists.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in deleting existing Demark product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }

        }
    }
}
