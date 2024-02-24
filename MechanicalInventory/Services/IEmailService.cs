using MechanicalInventory.Models.MailHelper;
using System.Runtime.CompilerServices;

namespace MechanicalInventory.Services
{
    public interface IEmailService
    {
        public bool SendEmailThroughBrevo(MailRequest mailRequest);
        public Task<bool> SendEmailThroughSengrid(MailRequest mailRequest);
    }
}
