using MyApp.Models;

public interface IUsersListRepository
{
    Task<int> CheckUserExistsAsync(string username, string email);
    Task<int> CreateUserAsync(string username, string email, string passwordHash, int companyId);
    Task<int> CreateCompanyAsync(string companyName);
    // Task<User> GetUserByCompanyAndUsernameAsync(string companyName, string username);
}