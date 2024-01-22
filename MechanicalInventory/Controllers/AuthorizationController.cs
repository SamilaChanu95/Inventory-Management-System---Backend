using MechanicalInventory.Handlers;
using MechanicalInventory.Models;
using MechanicalInventory.Services;
using Microsoft.AspNetCore.Mvc;

namespace MechanicalInventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthorizationController> _logger;
        private readonly IConfiguration _configuration;
        public AuthorizationController(IUserService userService, ILogger<AuthorizationController> logger, IConfiguration configuration)
        {
            _userService = userService;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserCredential userCredential)
        {
            try
            {
                if (userCredential != null)
                {
                    string tokenKey = _configuration.GetSection("JWT:SecurityKey").Value;
                    User user = await _userService.GetUserByUsernamePassowrd(userCredential);
                    if (user != null)
                    {
                        if (user.Status)
                        {
                            TokenHandler tokenHandler = new TokenHandler(userCredential, tokenKey);
                            var token = await tokenHandler.GenerateToken();
                            _logger.LogInformation("JWT token generated successfully.");
                            return Ok(token);
                        }
                        else
                        {
                            _logger.LogError("Unauthorized. User is in-active.");
                            return NotFound();
                        }
                    }
                    else
                    {
                        _logger.LogError("Unauthorized. User not found.");
                        return Unauthorized();
                    }
                }
                else
                {
                    _logger.LogError("User credentials are invalid.");
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

    }
}
