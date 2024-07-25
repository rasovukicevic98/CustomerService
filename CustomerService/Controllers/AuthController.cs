using CustomerService.Contracts.Services;
using CustomerService.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Authenticates a user based on username and password.
        /// </summary>
        /// <param name="loginDto"> The Login details of the user.</param>
        /// <returns>Returns a token if the login attempt was successful, otherwise returns a bad request.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var res = await _authService.LoginRequest(loginDto);
            if (res.IsFailure)
            {
                return BadRequest(res.Error);
            }
            return Ok(res.Value);
        }
    }
}
