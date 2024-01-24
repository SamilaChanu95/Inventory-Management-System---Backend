using MechanicalInventory.Models;
using MechanicalInventory.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MechanicalInventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YamahaController : ControllerBase
    {
        private readonly ILogger<YamahaController> _logger;
        private readonly IYamahaService _yamahaService;
        public YamahaController(ILogger<YamahaController> logger, IYamahaService yamahaService)
        {
            _logger = logger;
            _yamahaService = yamahaService;
        }

        [HttpGet]
        [Route("get-products-list")]
        public async Task<IActionResult> GetYahamaProductsList()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest(ModelState);
                }

                var result = await _yamahaService.GetProductsList();
                if (result == null || result.Count == 0)
                {
                    _logger.LogInformation("Yamaha product list is empty.");
                    return Ok("Yamaha product list is empty.");
                }
                else
                {
                    _logger.LogInformation("Successfully got the Yamaha product list.");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in loading Yamaha product list. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("get-product/{id:int}")]
        public async Task<IActionResult> GetYamahaProduct([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid."); ;
                    return BadRequest(ModelState);
                }

                if (await _yamahaService.IsExistsProduct(id))
                {
                    var result = await _yamahaService.GetProduct(id);
                    _logger.LogInformation($"Successfully got the Yamaha product with id : {id}");
                    return Ok(result);
                }
                else
                {
                    _logger.LogInformation("Requested Yamaha product doesn't exists.");
                    return NotFound("Requested Yamaha product doesn't exists.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{id} is not valid.");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("add-product")]
        public async Task<IActionResult> AddYamahaProduct([FromBody] YamahaProduct yamahaProduct) 
        {
            try
            {
                if (!ModelState.IsValid) 
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest(ModelState);
                }

                var result = await _yamahaService.AddProduct(yamahaProduct);
                _logger.LogInformation($"Successfully added new Yamaha product.");
                return Ok(result);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"Error in adding new Yamaha product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete-product/{id:int}")]
        public async Task<IActionResult> DeleteYamahaProduct([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid) 
                {
                    _logger.LogError("Model state is not vaild.");
                    return BadRequest("Model state is not vaild.");
                }

                if (await _yamahaService.IsExistsProduct(id))
                {
                    var result = await _yamahaService.DeleteProduct(id);
                    _logger.LogInformation($"Successfully deleted existing Yamaha product with id : {id}");
                    return Ok(result);
                }
                else 
                {
                    _logger.LogInformation("Requested Yamaha product doesn't exists.");
                    return NotFound("Requested Yamaha product doesn't exists.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in deleting existing Yamaha product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("update-product")]
        public async Task<IActionResult> UpdateYamahaProduct([FromBody] YamahaProduct yamahaProduct)
        {
            try
            {
                if (!ModelState.IsValid) 
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest("Model state is not valid.");
                }

                int productId = yamahaProduct.Id;
                if (await _yamahaService.IsExistsProduct(productId)) 
                {
                    var result = await _yamahaService.UpdateProduct(yamahaProduct);
                    _logger.LogInformation("Successfully updated existing Yamaha product.");
                    return Ok(result);
                }
                else 
                {
                    _logger.LogInformation("Requested Yamaha product doesn't exists.");
                    return NotFound("Requested Yamaha product doesn't exists.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in updating existing Yamaha product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
