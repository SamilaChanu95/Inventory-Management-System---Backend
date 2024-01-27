using MechanicalInventory.Models;
using MechanicalInventory.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace MechanicalInventory.Controllers
{
    [Route("api/[controller]")]
    [EnableRateLimiting("Api")]
    [ApiController]
    public class KtmController : ControllerBase
    {
        private readonly ILogger<KtmController> _logger;
        private readonly IKtmService _ktmService;
        public KtmController(ILogger<KtmController> logger, IKtmService ktmService) 
        {
            _ktmService = ktmService;
            _logger = logger;
        }

        [HttpGet]
        [Route("get-product-list")]
        public async Task<IActionResult> GetKtmProductsList()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest("Model state is not valid.");
                }
                var result = await _ktmService.GetProductsList();
                if (result == null)
                {
                    _logger.LogInformation("Ktm products list is empty.");
                    return Ok("Ktm products list is empty.");
                }
                else
                {
                    _logger.LogInformation("Successfully got the Ktm products list.");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in loading Ktm product list. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("get-product/{id:int}")]
        public async Task<IActionResult> GetKtmProduct([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest("Model state is not valid.");
                }
                if (await _ktmService.IsExistsProduct(id))
                {
                    var result = await _ktmService.GetProduct(id);
                    _logger.LogInformation($"Successfully got the Ktm product with id : {id}");
                    return Ok(result);
                }
                else
                {
                    _logger.LogInformation("Requested Ktm product doesn't exists.");
                    return NotFound("Requested Ktm product doesn't exists.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in getting the Ktm product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("add-product")]
        public async Task<IActionResult> AddKtmProduct([FromBody] KtmProduct ktmProduct)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest("Model state is not valid.");
                }

                var result = await _ktmService.AddProduct(ktmProduct);
                _logger.LogInformation("Successfully added new Ktm product.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in adding new Ktm product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("update-product")]
        public async Task<IActionResult> UpdateKtmProduct([FromBody] KtmProduct ktmProduct)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest("Model state is not valid.");
                }
                int productId = ktmProduct.Id;
                if (await _ktmService.IsExistsProduct(productId))
                {
                    var result = await _ktmService.UpdateProduct(ktmProduct);
                    _logger.LogInformation("Successfully updated existing Ktm product.");
                    return Ok(result);
                }
                else
                {
                    _logger.LogInformation("Requested Ktm product doesn't exists.");
                    return NotFound("Requested Ktm product doesn't exists.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in updating existing Ktm product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        [Route("delete-product/{id:int}")]
        public async Task<IActionResult> DeleteKtmProduct([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest("Model state is not valid.");
                }
                if (await _ktmService.IsExistsProduct(id))
                {
                    var result = await _ktmService.DeleteProduct(id);
                    _logger.LogInformation($"Successfully deleted existing Ktm product with id : {id}");
                    return Ok(result);
                }
                else
                {
                    _logger.LogInformation("Requested Ktm product doesn't exists.");
                    return NotFound("Requested Ktm product doesn't exists.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in deleting existing Ktm product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }

        }
    }
}
