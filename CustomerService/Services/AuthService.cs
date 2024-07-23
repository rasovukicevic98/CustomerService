using CSharpFunctionalExtensions;
using CustomerService.Contracts.Repositories;
using CustomerService.Contracts.Services;
using CustomerService.Dtos;
using CustomerService.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;

namespace CustomerService.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAgentRepository _agentRepository;
        private readonly IPasswordHasher<Agent> _passwordHasher;
        private readonly ITokenService _tokenService;

        public AuthService(IPasswordHasher<Agent> passwordHasher, IAgentRepository agentRepository, ITokenService tokenService)
        {
            _passwordHasher = passwordHasher;
            _agentRepository = agentRepository;
            _tokenService = tokenService;
        }

        public async Task<Result<string, IEnumerable<string>>> LoginRequest(LoginDto loginDto)
        {
            var agent = await _agentRepository.GetByUsernameAsync(loginDto.Username);
            if (agent == null)
            {
                return Result.Failure<string, IEnumerable<string>>(new List<string> { "Invalid username or password." });
            }

            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(agent, agent.Password, loginDto.Password);
            if (passwordVerificationResult != PasswordVerificationResult.Success)
            {
                return Result.Failure<string, IEnumerable<string>>(new List<string> { "Invalid username or password." });
            }

            var token = _tokenService.GenerateToken(agent);

            return Result.Success<string, IEnumerable<string>>(token);
        }
    }
}
