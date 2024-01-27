using MechanicalInventory.Models;
using MechanicalInventory.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace MechanicalInventory.Controllers
{
    [Route("api/[controller]")]
    [EnableRateLimiting("Api")]
    [ApiController]
    public class HondaController : ControllerBase
    {
        private readonly IHondaService _hondaService;
        private readonly ILogger<HondaController> _logger;
        public HondaController(IHondaService hondaService, ILogger<HondaController> logger)
        {
            _hondaService = hondaService;
            _logger = logger;
        }

        [HttpGet]
        [Route("get-products-list")]
        public async Task<ActionResult> GetHondaProductsList()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest(ModelState);
                }

                var result = await _hondaService.GetProductList();
                if (result == null)
                {
                    _logger.LogInformation("Honda product list is empty.");
                    return Ok("Honda product list is empty.");
                }
                else
                {
                    _logger.LogInformation("Successfully got the Honda product list.");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in loading Honda product list. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("get-product/{id:int}")]
        public async Task<ActionResult> GetHondaProduct([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest(ModelState);
                }

                if (await _hondaService.IsExistsProduct(id))
                {
                    var result = await _hondaService.GetProduct(id);
                    _logger.LogInformation($"Successfully got the Honda product with id : {id}");
                    return Ok(result);
                }
                else
                {
                    _logger.LogInformation("Requested Honda product doesn't exists.");
                    return NotFound("Requested Honda product doesn't exists.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in getting the Honda product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("add-product")]
        public async Task<ActionResult> AddHondaProduct([FromBody] HondaProduct hondaProduct)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest(ModelState);
                }

                var result = await _hondaService.AddProduct(hondaProduct);
                _logger.LogInformation($"Successfully added new Honda product.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in adding new Honda product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("update-product")]
        public async Task<ActionResult> UpdateHondaProduct([FromBody] HondaProduct hondaProduct)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest(ModelState);
                }

                var result = await _hondaService.UpdateProduct(hondaProduct);
                _logger.LogInformation($"Successfully updated existing Honda product.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in updating existing Honda product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete-product/{id:int}")]
        public async Task<ActionResult> DeleteHondaProduct([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest("Model state is not valid.");
                }

                if (await _hondaService.IsExistsProduct(id))
                {
                    var result = await _hondaService.DeleteProduct(id);
                    _logger.LogInformation($"Successfully deleted existing Honda product with id : {id}");
                    return Ok(result);
                }
                else
                {
                    _logger.LogInformation("Requested Honda product doesn't exists.");
                    return NotFound("Requested Honda product doesn't exists.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in deleting existing Honda product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}

