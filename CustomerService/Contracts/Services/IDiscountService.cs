using CSharpFunctionalExtensions;
using CustomerService.Dtos;

namespace CustomerService.Contracts.Services
{
    public interface IDiscountService
    {
        Task<Result<DiscountDto, IEnumerable<string>>> CreateDiscount(DiscountDto discountDto, string agentUsername);


    }
}
