using CustomerService.Entities;

namespace CustomerService.Contracts.Services
{
    public interface ITokenService
    {
        string GenerateToken(Agent agent);
    }
}
