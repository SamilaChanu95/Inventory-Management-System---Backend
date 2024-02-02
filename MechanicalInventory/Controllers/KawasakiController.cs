using MechanicalInventory.Models;
using MechanicalInventory.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace MechanicalInventory.Controllers
{
    [Route("api/[controller]")]
    [EnableRateLimiting(policyName: "fixed-rate-limiter")]
    [ApiController]
    public class KawasakiController : ControllerBase
    {
        private readonly ILogger<KawasakiController> _logger;
        private readonly IKawasakiService _kawasakiService;
        public KawasakiController(ILogger<KawasakiController> logger, IKawasakiService kawasakiService)
        {
            _logger = logger;
            _kawasakiService = kawasakiService;
        }

        [HttpGet]
        [Route("get-product-list")]
        public async Task<IActionResult> GetKawasakiProductsList()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest("Model state is not valid.");
                }
                var result = await _kawasakiService.GetProductsList();
                if (result == null)
                {
                    _logger.LogInformation("Kawasaki products list is empty.");
                    return Ok("Kawasaki products list is empty.");
                }
                else
                {
                    _logger.LogInformation("Successfully got the Kawasaki products list.");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in loading Kawasaki product list. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("get-product/{id:int}")]
        public async Task<IActionResult> GetKawasakiProduct([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest("Model state is not valid.");
                }
                if (await _kawasakiService.IsExistsProduct(id))
                {
                    var result = await _kawasakiService.GetProduct(id);
                    _logger.LogInformation($"Successfully got the Kawasaki product with id : {id}");
                    return Ok(result);
                }
                else
                {
                    _logger.LogInformation("Requested Kawasaki product doesn't exists.");
                    return NotFound("Requested Kawasaki product doesn't exists.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in getting the Kawasaki product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("add-product")]
        public async Task<IActionResult> AddKawasakiProduct([FromBody] KawasakiProduct kawasakiProduct)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest("Model state is not valid.");
                }

                var result = await _kawasakiService.AddProduct(kawasakiProduct);
                _logger.LogInformation("Successfully added new Kawasaki product.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in adding new Kawasaki product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("update-product")]
        public async Task<IActionResult> UpdateKawasakiProduct([FromBody] KawasakiProduct kawasakiProduct)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest("Model state is not valid.");
                }
                int productId = kawasakiProduct.Id;
                if (await _kawasakiService.IsExistsProduct(productId))
                {
                    var result = await _kawasakiService.UpdateProduct(kawasakiProduct);
                    _logger.LogInformation("Successfully updated existing Kawasaki product.");
                    return Ok(result);
                }
                else
                {
                    _logger.LogInformation("Requested Kawasaki product doesn't exists.");
                    return NotFound("Requested Kawasaki product doesn't exists.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in updating existing Kawasaki product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        [Route("delete-product/{id:int}")]
        public async Task<IActionResult> DeleteKawasakiProduct([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest("Model state is not valid.");
                }
                if (await _kawasakiService.IsExistsProduct(id))
                {
                    var result = await _kawasakiService.DeleteProduct(id);
                    _logger.LogInformation($"Successfully deleted existing Kawasaki product with id : {id}");
                    return Ok(result);
                }
                else
                {
                    _logger.LogInformation("Requested Kawasaki product doesn't exists.");
                    return NotFound("Requested Kawasaki product doesn't exists.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in deleting existing Kawasaki product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }

        }
    }
}
