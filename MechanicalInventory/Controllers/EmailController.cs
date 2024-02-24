using MechanicalInventory.Models.MailHelper;
using MechanicalInventory.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MechanicalInventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly ILogger<EmailController> _logger;
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService, ILogger<EmailController> logger) 
        {
            _emailService = emailService;
            _logger = logger;
        }

        [HttpPost]
        [Route("send-email-brevo")]
        public IActionResult SentdEmailThroughBrevo([FromBody] MailRequest mailRequest) 
        {
            try 
            {
                bool result = _emailService.SendEmailThroughBrevo(mailRequest);
                _logger.LogInformation("Email successfully sent.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in email sending. Error : {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("send-email-sengrid")]
        public async Task<IActionResult> SentdEmailThroughSengrid([FromBody] MailRequest mailRequest)
        {
            try
            {
                bool result = await _emailService.SendEmailThroughSengrid(mailRequest);
                _logger.LogInformation("Email successfully sent.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in email sending. Error : {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

    }
}
