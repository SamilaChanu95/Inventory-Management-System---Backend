using MechanicalInventory.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MechanicalInventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger) 
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        [Route("get-user/{id:int}")]
        public async Task<ActionResult> GetUserDetails([FromRoute] int id) 
        {
            try
            {
                var user = await _userService.GetUserDetails(id);
                return Ok(user);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
