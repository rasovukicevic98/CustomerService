using System.ComponentModel.DataAnnotations;

namespace CustomerService.Dtos
{
    /// <summary>
    /// Represents a discount coupon.
    /// </summary>
    public class DiscountDto
    {
        /// <summary>
        /// Represents ID of the User that will be awarded with id.
        /// </summary>
        [Required]
        public int UserId { get; set; }
        /// <summary>
        /// Represents the coupon User has to type in in order to use the discount.
        /// </summary>
        [Required]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Discount coupon must be exactly 10 characters long.")]
        public string DiscountCoupon { get; set; }
        /// <summary>
        /// Represents the discount % User will get at the checkout.
        /// </summary>
        [Required]
        [Range(5, 50, ErrorMessage = "Discount percentage must be between 5 and 50.")]
        public int DiscountPercentage { get; set; }
    }
}
