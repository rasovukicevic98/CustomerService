using CSharpFunctionalExtensions;
using CustomerService.Contracts.Services;
using CustomerService.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _service;

        public DiscountController(IDiscountService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(DiscountDto discountDto)
        {
            var res = await _service.CreateDiscount(discountDto);
            if (res.IsFailure)
            {
                return BadRequest(res.Error);
            }
            return Ok(res.Value);
            
        }

    }
}
