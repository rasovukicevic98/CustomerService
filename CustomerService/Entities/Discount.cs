namespace CustomerService.Entities
{
    public class Discount
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string DiscountCoupon { get; set; }
        public DateTime CouponStartDate { get; set; }
        public DateTime CouponEndDate { get; set; }
        public int DiscountPercentage { get; set; }
        public bool IsUsed { get; set; } = false;
        public int AgentId { get; set; }
        public Agent Agent {  get; set; }
    }
}
