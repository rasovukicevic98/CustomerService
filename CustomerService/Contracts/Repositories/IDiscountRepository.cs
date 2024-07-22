using CustomerService.Entities;

namespace CustomerService.Contracts.Repositories
{
    public interface IDiscountRepository
    {
        Task<bool> Add(Discount discount);
        bool ExistByCoupon(string coupon);
    }
}
