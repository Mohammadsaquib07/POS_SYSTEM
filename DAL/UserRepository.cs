using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Products_Crud.DAL;

public class UserRepository : IUsersListRepository
{
    private readonly UserDbContext _UserDbContextObj;

    public UserRepository(UserDbContext instance)
    {
        _UserDbContextObj = instance;
    }

    public async Task<int> CheckUserExistsAsync(string username, string email)
    {
        var result = await _UserDbContextObj.Database
            .SqlQueryRaw<int>(
                "EXEC usp_users_Exist @Username, @Email",
                new SqlParameter("@Username", username),
                new SqlParameter("@Email", email))
            .ToListAsync();

        return result.FirstOrDefault();
    }

    public async Task<int> CreateUserAsync(string username, string email, string passwordHash)
    {
        var result = await _UserDbContextObj.Database
            .SqlQueryRaw<int>(
                "EXEC Usp_Create_User @Username, @Email, @PasswordHash",
                new SqlParameter("@Username", username),
                new SqlParameter("@Email", email),
                new SqlParameter("@PasswordHash", passwordHash))
            .ToListAsync();

        return result.FirstOrDefault(); // the new UserId
    }
}