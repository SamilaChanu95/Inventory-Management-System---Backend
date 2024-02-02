using MechanicalInventory.Models;
using MechanicalInventory.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace MechanicalInventory.Controllers
{
    [Route("api/[controller]")]
    [EnableRateLimiting(policyName: "fixed-rate-limiter")]
    [ApiController]
    public class HeroController : ControllerBase
    {
        private readonly IHeroService _heroService;
        private readonly ILogger<HeroController> _logger;
        public HeroController(IHeroService heroService, ILogger<HeroController> logger)
        {
            _heroService = heroService;
            _logger = logger;
        }

        [HttpGet]
        [Route("get-products-list")]
        public async Task<ActionResult> GetHeroProductsList()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest(ModelState);
                }

                var result = await _heroService.GetProductList();
                if (result == null)
                {
                    _logger.LogInformation("Hero product list is empty.");
                    return Ok("Hero product list is empty.");
                }
                else
                {
                    _logger.LogInformation("Successfully got the Hero product list.");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in loading Hero product list. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("get-product/{id:int}")]
        public async Task<ActionResult> GetHeroProduct([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest(ModelState);
                }

                if (await _heroService.IsExistsProduct(id))
                {
                    var result = await _heroService.GetProduct(id);
                    _logger.LogInformation($"Successfully got the Hero product with id : {id}");
                    return Ok(result);
                }
                else
                {
                    _logger.LogInformation("Requested Hero product doesn't exists.");
                    return NotFound("Requested Hero product doesn't exists.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in getting the Hero product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("add-product")]
        public async Task<ActionResult> AddHeroProduct([FromBody] HeroProduct heroProduct)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest(ModelState);
                }

                var result = await _heroService.AddProduct(heroProduct);
                _logger.LogInformation($"Successfully added new Hero product.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in adding new Hero product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("update-product")]
        public async Task<ActionResult> UpdateHeroProduct([FromBody] HeroProduct heroProduct)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest(ModelState);
                }

                var result = await _heroService.UpdateProduct(heroProduct);
                _logger.LogInformation($"Successfully updated existing Hero product.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in updating existing Hero product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete-product/{id:int}")]
        public async Task<ActionResult> DeleteHeroProduct([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model state is not valid.");
                    return BadRequest(ModelState);
                }

                if (await _heroService.IsExistsProduct(id))
                {
                    var result = await _heroService.DeleteProduct(id);
                    _logger.LogInformation($"Successfully deleted existing Hero product with id : {id}");
                    return Ok(result);
                }
                else
                {
                    _logger.LogInformation("Requested Hero product doesn't exists.");
                    return NotFound("Requested Hero product doesn't exists.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in deleting existing Hero product. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
