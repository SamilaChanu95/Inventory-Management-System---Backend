
using MechanicalInventory.Models.MailHelper;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;
using Mailjet;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Model;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace MechanicalInventory.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSetting _emailSetting;
        private ILogger<EmailService> _logger;
        private IConfiguration _configuration;
        public EmailService(IOptions<EmailSetting> options, ILogger<EmailService> logger, IConfiguration configuration) 
        {
            _emailSetting = options.Value;
            _logger = logger;
            _configuration = configuration;
        }

        // This method for send the email message
        public bool SendEmailThroughBrevo(MailRequest mailRequest)
        {
            // This is normal way of sending emails via using MailKit & MimeKit Nuget Packages
            /* try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_emailSetting.Email);
                email.To.Add(MailboxAddress.Parse(mailRequest.Receiver));
                email.Subject = mailRequest.Subject;
                var builder = new BodyBuilder();
                builder.HtmlBody = mailRequest.Body + AddEmailSignature();
                email.Body = builder.ToMessageBody();


                using (SmtpClient client = new SmtpClient()) 
                {
                    client.
                    await client.ConnectAsync(_emailSetting.Host, _emailSetting.Port, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(_emailSetting.Email, _emailSetting.Password);
                    await client.SendAsync(email);
                    await client.DisconnectAsync(true);
                    _logger.LogInformation("Successfully sent the email.");
                    return true;
                }   
            } 
            catch (Exception ex) 
            {
                _logger.LogInformation($"Error in email sending. Error : {ex.Message}");
                return false;
            } */

            // This is way to send the email using Bervo
            string? apiKey = _configuration.GetSection("Bervo:HttpApiKey").Value;

            Configuration.Default.ApiKey.Add("api-key", apiKey);

            var apiInstance = new TransactionalEmailsApi();

            // string SenderName = "Samila Chanuka95";
            // string SenderEmail = "chanukasamila95@gmail.com";
            string SenderEmail = _emailSetting.Email ?? "chanukasamila95@gmail.com";
            SendSmtpEmailSender Email = new SendSmtpEmailSender(null, SenderEmail);

            /*string ToEmail = "chanukasamila@gmail.com";
            string ToName = "Samila Chanuka";*/

            string ToEmail = mailRequest.Receiver;
            SendSmtpEmailTo smtpEmailTo = new SendSmtpEmailTo(ToEmail);
            List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>();
            To.Add(smtpEmailTo);

            /*string BccName = "Samila Chanuka072";
            string BccEmail = "chanukasamila072@gmail.com";
            SendSmtpEmailBcc BccData = new SendSmtpEmailBcc(BccEmail, BccName);
            List<SendSmtpEmailBcc> Bcc = new List<SendSmtpEmailBcc>();
            Bcc.Add(BccData);*/

            /*string CcName = "Samila Chanuka072";
            string CcEmail = "chanukasamila072@gmail.com";
            SendSmtpEmailCc CcData = new SendSmtpEmailCc(CcEmail, CcName);
            List<SendSmtpEmailCc> Cc = new List<SendSmtpEmailCc>();
            Cc.Add(CcData);*/

            string HtmlContent = "<h1>" + mailRequest.Body + "</h1>" + AddEmailSignature();
            string TextContent = null;

            //string Subject = "Testing Brevo at third-time.";
            string Subject = mailRequest.Subject ?? "No Subject";

            /*string ReplyToName = "Samila Chanuka95";
            string ReplyToEmail = "chanukasamila95@gmail.com";*/
            string ReplyToEmail = _emailSetting.Email;
            SendSmtpEmailReplyTo ReplyTo = new SendSmtpEmailReplyTo(ReplyToEmail);


            // Do the string convert into byte array
            string AttachmentUrl = null;

            byte[] Content = System.Text.ASCIIEncoding.ASCII.GetBytes(mailRequest.Body ?? "");
            string AttachmentName = "test.txt";
            SendSmtpEmailAttachment AttachmentContent = new SendSmtpEmailAttachment(AttachmentUrl, Content, AttachmentName);
            List<SendSmtpEmailAttachment> Attachment = new List<SendSmtpEmailAttachment>();
            Attachment.Add(AttachmentContent);

            try
            {
                var sendSmtpEmail = new SendSmtpEmail(Email, To, null, null, HtmlContent, TextContent, Subject, ReplyTo, Attachment);
                CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
                return true;
            }

            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        // This method for apply the email signature
        public string AddEmailSignature() 
        {
            string style = "<br/><hr/><div><table aria-hidden=\"true\" aria-hidden=\"true\" cellspacing=\"0\" cellpadding=\"0\" width=\"600\">";
            style += "<tr><td><img style=\"height: 70px; width: 70px; border-radius: 100%;\" src=\"https://lh3.googleusercontent.com/drive-viewer/AEYmBYSqvMxo7Wue9dDYxj_7mXu8ZD6Ot3SImLt5GIVHhzww8bAsFav1QLuJkB5ytcTCTb7DipzgO7v5ck4XfWqGb4dQBjqs=s2560\" alt=\"profile image\"></td>";
            style += "<td><div style=\"padding-right: 72px; padding-bottom: 5px; padding-top: 1px;\">";
            style += "<span style=\"text-transform: capitalize; font-weight: 500; color:rgb(172, 162, 162); font-family: Verdana, Geneva, Tahoma, sans-serif; font-size:small;\">s.a.s.chanuka</span><br/>";
            style += "<span style=\"text-transform: capitalize; font-weight: 500; color:rgb(172, 162, 162); font-family: Verdana, Geneva, Tahoma, sans-serif; font-size:small; text-decoration:none;\">B.Sc</span>&nbsp;<span style=\"text-transform: capitalize; font-weight: 500; color:rgb(172, 162, 162); font-family: Verdana, Geneva, Tahoma, sans-serif; font-size:small;\">Hons(computer science) - University of Jaffna</span><br/>";
            style += "<span style=\"text-transform: none; font-weight: 500; font-family: Verdana, Geneva, Tahoma, sans-serif; font-size:small;\"><a href=\"https://linkedin.com/in/chanuka-samila\" style=\"color: blue;\">LinkedIn</a></span>&nbsp;|&nbsp;";
            style += "<span style=\"text-transform: none; font-weight: 500; font-family: Verdana, Geneva, Tahoma, sans-serif; font-size:small;\"><a href=\"https://github.com/SamilaChanu95\" style=\"color: blue;\">GitHub</a></span><br/>";
            style += "<span style=\"text-transform: capitalize; font-weight: 500; color:rgb(172, 162, 162); font-family: Verdana, Geneva, Tahoma, sans-serif; font-size:small;\">mobile: +94727671463</span>";
            style += "<br/></div></td></tr></table></div>";
            return style;
        }

        // This method for send the emails for multiple users with Sendgrid
        public async Task<bool> SendEmailThroughSengrid(MailRequest mailRequest) 
        {
            string? api = _configuration.GetSection("Sendgrid:ApiKey").Value;
            string? from = _configuration.GetSection("Sendgrid:FromEmail").Value;
            var client = new SendGridClient(api);
            var sender = new EmailAddress(from, "Chanuka Samila");
            var receiver = new EmailAddress(mailRequest.Receiver, null);
            var receiverSub01 = new EmailAddress(_configuration.GetSection("Sendgrid:ToEmail01").Value, _configuration.GetSection("Sendgrid:ToEmail01Name").Value);
            var receiverSub02 = new EmailAddress(_configuration.GetSection("Sendgrid:ToEmail02").Value, _configuration.GetSection("Sendgrid:ToEmail02Name").Value);
            List<EmailAddress> receivers = [receiver, receiverSub01, receiverSub02];
            var body = "<h1>" + mailRequest.Body + "</h1>" + AddEmailSignature();
            // var message = MailHelper.CreateSingleEmail(sender, receiver, mailRequest.Subject, null, body);
            var commonMessage = MailHelper.CreateSingleEmailToMultipleRecipients(sender, receivers, mailRequest.Subject, null, body);
            try 
            {
                var response = await client.SendEmailAsync(commonMessage);
                return (response.IsSuccessStatusCode && response.StatusCode.ToString() == "Accepted");
            }
            catch (Exception e) 
            {
                return false;
            }
        }
    }
}
