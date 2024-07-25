using CSharpFunctionalExtensions;
using CustomerService.Contracts.Services;
using CustomerService.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CustomerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _service;

        public DiscountController(IDiscountService service)
        {
            _service = service;
        }

        /// <summary>
        /// Create a new Discount coupon.
        /// </summary>
        /// <param name="discountDto">The details of the discount coupon to create.</param>
        /// <returns>Returns the created discount coupon if successful; otherwise, returns a bad request.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(DiscountDto discountDto)
        { 
            var username = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            var res = await _service.CreateDiscount(discountDto, username);
            if (res.IsFailure)
            {
                return BadRequest(res.Error);
            }
            return Ok(res.Value);
            
        }

    }
}
