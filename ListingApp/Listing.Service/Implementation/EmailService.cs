using Listing.Domain;
using Listing.Domain.DomainModels;
using Listing.Repository.Interface;
using Listing.Service.Interface;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Listing.Service.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(EmailSettings settings)
        {
            _settings = settings;
        }

        public async Task SendEmailAsync(ListingPost listing, string action, string userEmail)
        {
            MimeMessage emailMessage = createMessage(listing, action, userEmail);
            try
            {
                using (var smtp = new MailKit.Net.Smtp.SmtpClient())
                {
                    var socketOption = _settings.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto;
                    await smtp.ConnectAsync(_settings.SmtpServer, _settings.SmtpServerPort, socketOption);

                    if (!string.IsNullOrEmpty(_settings.SmtpUserName))
                    {
                        await smtp.AuthenticateAsync(_settings.SmtpUserName, _settings.SmtpPassword);
                    }

                    await smtp.SendAsync(emailMessage);


                    await smtp.DisconnectAsync(true);
                }

            }
            catch (SmtpException ex)
            {
                throw ex;
            }
        }
        public MimeMessage createMessage(ListingPost listing, string action, string userEmail)
        {
            string messageSubject = "";
            string header = "";
            string emailTo = "";
            if (action == "newListing")
            {
                messageSubject = "Listing to validate";
                header = "You have one new listing to validate";
                emailTo = "karolinaangelova864@gmail.com";
            }
            else
            {
                messageSubject = "Listing "+action;
                header = "Your listing has been "+action;
                emailTo = userEmail;
            }
            var emailMessage = new MimeMessage
            {
                Sender = new MailboxAddress(_settings.SendersName, _settings.SmtpUserName),
                Subject = messageSubject
            };
            emailMessage.From.Add(new MailboxAddress(_settings.EmailDisplayName, _settings.SmtpUserName));

            StringBuilder htmlContent = new StringBuilder();

            htmlContent.Append("<!DOCTYPE html><html lang=\"en\"><head><meta charset=\"utf - 8\" /></head>");
            htmlContent.Append("<body><table width=\"100 % \" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"color: #2b3346; background-color: #e1e4ef; font-size: 16px\">");
            htmlContent.Append("<tr height=\"40\"></tr><td style=\"width: 560px\"><center><img width=\"140\" " +
                "src=\"https://uploads-eu-west-1.insided.com/inspired-en/attachment/797fa735-fa6e-427b-9796-9c93b9965698_thumb.png\"/>" +
                "</center></td></tr><tr height=\"20\"></tr>");
            htmlContent.Append("<tr><td style=\"padding: 26px 40px; width: 40px\"><center><div style=\"width: 500px; background: #ffffff;\">");
            htmlContent.Append("<h3>" + header + "</h3>");
            htmlContent.Append("<p>The listing has title " + listing.Title + "</p>");
            htmlContent.Append("<p>The listing has description " + listing.Description + "</p>");
            htmlContent.Append("<p>The listing is created at " + listing.DateCreated.ToString() + "</p></div>");
            htmlContent.Append("</center></td></tr><tr height=\"60\"></tr></table></body></html>");

            emailMessage.Body = new TextPart(TextFormat.Html) { Text = htmlContent.ToString() };

            emailMessage.To.Add(MailboxAddress.Parse(emailTo));

            return emailMessage;
        }
    }
}
