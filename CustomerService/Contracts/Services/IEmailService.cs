﻿

namespace CustomerService.Contracts.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string htmlMessage);
        Task SendEmailWithAttachmentAsync(string toEmail, string subject, string htmlMessage, byte[] attachmentBytes, string attachmentFilename);
        Task SendReportsToAllAgents(IEnumerable<string> agentsEmails);
    }
}
