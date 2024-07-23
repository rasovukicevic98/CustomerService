using CustomerService.Entities;

namespace CustomerService.Contracts.Repositories
{
    public interface IAgentRepository
    {
        Task<Agent> GetByUsernameAsync(string username);
    }
}
