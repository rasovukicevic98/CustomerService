using CustomerService.Contracts.Repositories;
using CustomerService.Data;
using CustomerService.Entities;

namespace CustomerService.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly AppDbContext _context;

        public DiscountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(Discount discount)
        {
            _context.Discounts.Add(discount);
            return Save();
        }

        public bool ExistByCoupon(string coupon)
        {
            var couponExists = _context.Discounts.FirstOrDefault(dc => dc.DiscountCoupon == coupon);
            if (couponExists != null)
            {
                return true;
            }
            return false;
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
