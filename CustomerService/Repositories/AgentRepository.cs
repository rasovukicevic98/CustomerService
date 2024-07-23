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

        public IQueryable<string> GetAgentsEmails()
        {
            return _context.Agents
                .AsNoTracking()
                .Select(a => a.Email);
        }

        public async Task<Agent> GetByEmail(string email)
        {
            return await _context.Agents.FirstOrDefaultAsync(a => a.Email == email);
        }
        
    }
}
