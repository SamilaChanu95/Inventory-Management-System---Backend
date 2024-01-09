using MechanicalInventory.Models;
using MechanicalInventory.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MechanicalInventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
                _logger.LogInformation("Successfully got the products list.");
                return Ok(result);
            } 
            catch (Exception ex) 
            {
                _logger.LogError($"Error in loading products list. Error: {ex.Message}");
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
                _logger.LogInformation("Successfully added new product.");
                return Ok(result);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"Error in adding new product. Error: {ex.Message}");
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
                    _logger.LogInformation("Successfully got the required product.");
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("Requested product doesn't exist.");
                    return NotFound();
                }  
            }
            catch (Exception ex) 
            {
                _logger.LogError($"Error in getting product. Error: {ex.Message}");
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
                    _logger.LogInformation("Successfully deleted the required product.");
                    return Ok(result);
                }
                else 
                {
                    _logger.LogError("Requested product doesn't exist.");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in deleting product. Error: {ex.Message}");
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
                    _logger.LogInformation("Successfully updated the existing product.");
                    return Ok(result);
                }
                else 
                {
                    _logger.LogError("Requested product doesn't exist.");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in updating existing product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
