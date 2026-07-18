public interface IUsersListRepository
{
    Task<int> CheckUserExistsAsync(string username,string email);
     Task<int> CreateUserAsync(string username, string email, string passwordHash);
}