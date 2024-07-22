using System.ComponentModel.DataAnnotations;

namespace CustomerService.Dtos
{
    public class DiscountDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Discount coupon must be exactly 10 characters long.")]
        public string DiscountCoupon { get; set; }
        [Required]
        [Range(5, 50, ErrorMessage = "Discount percentage must be between 5 and 50.")]
        public int DiscountPercentage { get; set; }
    }
}
