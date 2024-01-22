using MechanicalInventory.Models;
using MechanicalInventory.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MechanicalInventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TvsController : ControllerBase
    {
        private readonly ILogger<TvsController> _logger;
        private readonly ITvsService _tvsService;

        public TvsController(ITvsService tvsService, ILogger<TvsController> logger)
        {
            _tvsService = tvsService;
            _logger = logger;
        }

        [HttpGet]
        [Route("get-products-list")]
        public async Task<IActionResult> GetTvsProductList()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest(ModelState);
                }

                var result = await _tvsService.GetTvsProductsList();
                if (result == null)
                {
                    _logger.LogInformation("Product list is empty.");
                    // return Ok(new HttpResponseMessage() {  StatusCode = HttpStatusCode.OK ,Content = new StringContent("Product list is empty.") });
                    return Ok("Product list is empty.");
                }
                else
                {
                    _logger.LogInformation("Successfully got the product list.");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in loading product list. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("get-product/{id:int}")]
        public async Task<IActionResult> GetTvsProduct([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest(ModelState);
                }

                if (await _tvsService.IsExistsProduct(id))
                {
                    var tvsProduct = await _tvsService.GetTvsProduct(id);
                    _logger.LogInformation($"Successfully got the Tvs product with id : {id}");
                    return Ok(tvsProduct);
                }
                else
                {
                    _logger.LogError("Requested Tvs product doesn't exists.");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in getting the Tvs product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("add-product")]
        public async Task<IActionResult> AddTvsProduct([FromBody] TvsProduct tvsProduct) 
        {
            try 
            {
                if (!ModelState.IsValid) 
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest(ModelState);
                }

                var result = await _tvsService.AddTvsProduct(tvsProduct);
                _logger.LogInformation("Suceesfully added new Tvs product.");
                return Ok(result);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"Error in adding new product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("update-product")]
        public async Task<IActionResult> UpdateTvsProduct([FromBody] TvsProduct tvsProduct)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest(ModelState);
                }

                var result = await _tvsService.UpdateTvsProduct(tvsProduct);
                _logger.LogInformation("Suceesfully updated existing Tvs product.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in updating existing product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete-product/{id:int}")]
        public async Task<IActionResult> DeleteTvsProduct([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest(ModelState);
                }

                if (await _tvsService.IsExistsProduct(id))
                {
                    var result = await _tvsService.DeleteTvsProduct(id);
                    _logger.LogInformation("Suceesfully deleted Tvs product.");
                    return Ok(result);
                }
                else 
                {
                    _logger.LogError("This Tvs product doesn't exists.");
                    return NotFound();
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in deleting Tvs product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
