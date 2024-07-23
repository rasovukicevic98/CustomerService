using CSharpFunctionalExtensions;
using CustomerService.Dtos;

namespace CustomerService.Contracts.Services
{
    public interface IAuthService
    {
        Task<Result<string, IEnumerable<string>>> LoginRequest(LoginDto loginDto);
    }
}
