using MechanicalInventory.Models;
using MechanicalInventory.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace MechanicalInventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting(policyName: "fixed-rate-limiter")]
    public class BajajProductController : ControllerBase
    {
        private readonly IBajajService _bajajService;
        private readonly ILogger<BajajProductController> _logger;

        public BajajProductController(IBajajService bajajService, ILogger<BajajProductController> logger)
        {
            _bajajService = bajajService;
            _logger = logger;
        }

        [HttpGet]
        [Route("get-products-list")]
        [EnableRateLimiting(policyName: "concurrency-limiter")]
        public async Task<IActionResult> GetProductsList()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest();
                }

                var result = await _bajajService.GetBajajProductsList();
                _logger.LogInformation("Successfully got the Bajaj products list.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in loading Bajaj products list. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("add-product")]
        public async Task<IActionResult> AddProduct([FromBody] BajajProduct bajajProduct)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest();
                }

                var result = await _bajajService.AddBajajProduct(bajajProduct);

                if (result)
                {
                    _logger.LogInformation("Successfully added new Bajaj product.");
                    return Ok(result);
                }
                else 
                {
                    _logger.LogInformation("Cannot added new Bajaj product.");
                    return Unauthorized("Cannot added new Bajaj product.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in adding new Bajaj product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("get-product/{id:int}")]
        public async Task<IActionResult> GetProduct([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest();
                }

                if (await _bajajService.IsExistProduct(id))
                {
                    var result = await _bajajService.GetBajajProduct(id);
                    _logger.LogInformation($"Successfully got the Bajaj product with id : {id}");
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("Requested Bajaj product doesn't exist.");
                    return NotFound("Requested Bajaj product doesn't exist.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in getting the Bajaj product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete-product/{id:int}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest();
                }

                if (await _bajajService.IsExistProduct(id))
                {
                    var result = await _bajajService.DeleteBajajProduct(id);
                    if (result)
                    {
                        _logger.LogInformation($"Successfully deleted existing Bajaj product with id : {id}.");
                        return Ok(result);
                    }
                    else 
                    {
                        _logger.LogInformation($"Cannot delete existing Bajaj product with id : {id}.");
                        return Unauthorized($"Cannot delete existing Bajaj product with id : {id}.");
                    }  
                }
                else
                {
                    _logger.LogError("Requested Bajaj product doesn't exist.");
                    return NotFound("Requested Bajaj product doesn't exist.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in deleting existing Bajaj product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("update-product")]
        public async Task<IActionResult> UpdateProduct([FromBody] BajajProduct bajajProduct)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest();
                }

                if (await _bajajService.IsExistProduct(bajajProduct.Id))
                {
                    var result = await _bajajService.UpdateBajajProduct(bajajProduct);
                    if (result)
                    {
                        _logger.LogInformation("Successfully updated existing Bajaj product.");
                        return Ok(result);
                    }
                    else 
                    {
                        _logger.LogInformation($"Cannot updated existing Bajaj product with id : {bajajProduct.Id}.");
                        return Unauthorized($"Cannot updated existing Bajaj product with id : {bajajProduct.Id}.");
                    }
                }
                else
                {
                    _logger.LogError("Requested Bajaj product doesn't exist.");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in updating existing Bajaj product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
