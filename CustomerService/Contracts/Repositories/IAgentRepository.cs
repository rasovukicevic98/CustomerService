using CustomerService.Entities;

namespace CustomerService.Contracts.Repositories
{
    public interface IAgentRepository
    {
        IQueryable<string> GetAgentsEmails();
        Task<Agent> GetByEmail(string email);
        Task<Agent> GetByUsernameAsync(string username);
    }
}
