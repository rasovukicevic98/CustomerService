using CustomerService.Contracts.Repositories;
using CustomerService.Data;
using CustomerService.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Repositories
{
    public class AgentRepository : IAgentRepository
    {
        private readonly AppDbContext _context;
        public AgentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Agent> GetByUsernameAsync(string username)
        {
            return await _context.Agents.FirstOrDefaultAsync(a => a.Username == username);
        }
        
    }
}
