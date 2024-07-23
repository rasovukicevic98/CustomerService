using CustomerService.Entities;

namespace CustomerService.Contracts.Repositories
{
    public interface IDiscountRepository
    {
        Task<bool> Add(Discount discount);
        Task<int> CouponsMadeToday(Agent agent);
        bool ExistByCoupon(string coupon);
        IEnumerable<Discount> GetCouponUsage();
    }
}
