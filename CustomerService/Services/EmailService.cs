using CustomerService.Configuration;
using CustomerService.Contracts.Services;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using MailKit.Net.Smtp;
using MimeKit;
using CustomerService.Contracts.Repositories;

namespace CustomerService.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly SmtpSettings _smtpSettings;
        private readonly IAgentRepository _agentRepository;
        private readonly IReportService _reportService;

        public EmailService(IConfiguration configuration, IOptions<SmtpSettings> smtpSettings, IAgentRepository agentRepository, IReportService reportService)
        {
            _configuration = configuration;
            _smtpSettings = smtpSettings.Value;
            _agentRepository = agentRepository;
            _reportService = reportService;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string htmlMessage)
        {

            try
            {
                var email = new MimeMessage();

                email.From.Add(new MailboxAddress(_smtpSettings.FromName, _smtpSettings.FromEmail));
                email.To.Add(new MailboxAddress("rasovukicevic98@outlook.com", "rasovukicevic98@outlook.com"));

                email.Subject = subject;

                var builder = new BodyBuilder()
                {
                    HtmlBody = htmlMessage
                };

                email.Body = builder.ToMessageBody();
                var smtp = new MailKit.Net.Smtp.SmtpClient();
                smtp.Connect(_smtpSettings.Server, _smtpSettings.Port, false);

                smtp.Authenticate(_smtpSettings.Username, _smtpSettings.Password);
                var res = smtp.Send(email);
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        public async Task SendEmailWithAttachmentAsync(string toEmail, string subject, string htmlMessage, byte[] attachmentBytes, string attachmentFilename)
        {
            try
            {
                var email = new MimeMessage();

                email.From.Add(new MailboxAddress(_smtpSettings.FromName, _smtpSettings.FromEmail));
                email.To.Add(new MailboxAddress("rasovukicevic98@outlook.com", "rasovukicevic98@outlook.com"));
                email.Subject = subject;

                var builder = new BodyBuilder
                {
                    HtmlBody = htmlMessage
                };

                using (var stream = new MemoryStream(attachmentBytes))
                {
                    builder.Attachments.Add(attachmentFilename, stream);
                }

                email.Body = builder.ToMessageBody();
                using (var smtp = new MailKit.Net.Smtp.SmtpClient())
                {
                    smtp.Connect(_smtpSettings.Server, _smtpSettings.Port, false);
                    smtp.Authenticate(_smtpSettings.Username, _smtpSettings.Password);
                    await smtp.SendAsync(email);
                    smtp.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public async Task SendReportsToAllAgents(IEnumerable<string> agentsEmails)
        {
            foreach (var agentEmail in agentsEmails)
            {
                var reportResult = await _reportService.GenerateReportAsCsvForEmail(agentEmail);
                if (reportResult != null)
                {
                    await SendEmailWithAttachmentAsync(agentEmail, "Campaign Summary", "Here is your summary of the campaign", reportResult.ToArray(), "CampaignReport.csv");
                }
            }
        }

    }
}
