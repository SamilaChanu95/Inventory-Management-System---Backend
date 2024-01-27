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
    public class SuzukiController : ControllerBase
    {
        private readonly ILogger<SuzukiController> _logger;
        private readonly ISuzukiService _suzukiService;
        public SuzukiController(ILogger<SuzukiController> logger, ISuzukiService suzukiService)
        {
            _logger = logger;
            _suzukiService = suzukiService;
        }

        [HttpGet]
        [Route("get-product-list")]
        public async Task<IActionResult> GetSuzukiProductsList()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest("Model state is not valid.");
                }
                var result = await _suzukiService.GetProductsList();
                if (result == null)
                {
                    _logger.LogInformation("Suzuki products list is empty.");
                    return Ok("Suzuki products list is empty.");
                }
                else
                {
                    _logger.LogInformation("Successfully got the Suzuki products list.");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in loading Suzuki product list. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("get-product/{id:int}")]
        public async Task<IActionResult> GetSuzukiProduct([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest("Model state is not valid.");
                }
                if (await _suzukiService.IsExistsProduct(id))
                {
                    var result = await _suzukiService.GetProduct(id);
                    _logger.LogInformation($"Successfully got the Suzuki product with id : {id}");
                    return Ok(result);
                }
                else
                {
                    _logger.LogInformation("Requested Suzuki product doesn't exists.");
                    return NotFound("Requested Suzuki product doesn't exists.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in getting the Suzuki product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("add-product")]
        public async Task<IActionResult> AddSuzukiProduct([FromBody] SuzukiProduct suzukiProduct)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest("Model state is not valid.");
                }

                var result = await _suzukiService.AddProduct(suzukiProduct);
                _logger.LogInformation("Successfully added new Suzuki product.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in adding new Suzuki product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("update-product")]
        public async Task<IActionResult> UpdateSuzukiProduct([FromBody] SuzukiProduct suzukiProduct)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest("Model state is not valid.");
                }
                int productId = suzukiProduct.Id;
                if (await _suzukiService.IsExistsProduct(productId))
                {
                    var result = await _suzukiService.UpdateProduct(suzukiProduct);
                    _logger.LogInformation("Successfully updated existing Suzuki product.");
                    return Ok(result);
                }
                else
                {
                    _logger.LogInformation("Requested Suzuki product doesn't exists.");
                    return NotFound("Requested Suzuki product doesn't exists.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in updating existing Suzuki product. Error: {ex.Message}");
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
                if (await _suzukiService.IsExistsProduct(id))
                {
                    var result = await _suzukiService.DeleteProduct(id);
                    _logger.LogInformation($"Successfully deleted existing Suzuki product with id : {id}");
                    return Ok(result);
                }
                else
                {
                    _logger.LogInformation("Requested Suzuki product doesn't exists.");
                    return NotFound("Requested Suzuki product doesn't exists.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in deleting existing Suzuki product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }

        }
    }
}
