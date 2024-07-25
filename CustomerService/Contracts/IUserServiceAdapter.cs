namespace CustomerService.Contracts
{
    public interface IUserServiceAdapter
    {
        Task<(bool exists, string userName)> UserExistsAsync(int userId);
    }
}
