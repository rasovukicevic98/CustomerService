using CustomerService.Constants;
using CustomerService.Contracts.Repositories;
using CustomerService.Contracts.Services;
using CustomerService.Repositories;
using Hangfire;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Services
{
    public class EmailSchedulerHostedService : IHostedService
    { 
        private readonly IServiceScopeFactory _scopeFactory;

        public EmailSchedulerHostedService(IServiceScopeFactory scopeFactory)
        {
            
            _scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            ScheduleEmail();
            return Task.CompletedTask;
        }

        private void ScheduleEmail()
        {
            
            if (DateTime.Now > CampaignPeriod.CampaignEndDate.AddMonths(1))
            {
                BackgroundJob.Enqueue(() => SendEmailToAllAgents().Wait());
            }
            
        }

        private async Task SendEmailToAllAgents()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                var agentRepository = scope.ServiceProvider.GetRequiredService<IAgentRepository>();
                
                                
                var agentsEmails = await agentRepository.GetAgentsEmails().ToListAsync();
               

                await emailService.SendReportsToAllAgents(agentsEmails);
            }
            
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
