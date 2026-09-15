using ERP.ERP.Modules.Shared;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUsersListRepository
{
    private readonly ISharedDbContext iSharedDbContext;

    public UserRepository(ISharedDbContext ISharedDbContextObj)
    {
        iSharedDbContext = ISharedDbContextObj;
    }

    public async Task<int> CheckUserExistsAsync(string username, string email)
    {
        var result = await iSharedDbContext.Database
            .SqlQueryRaw<int>(
                "EXEC usp_users_Exist @Username, @Email",
                new SqlParameter("@Username", username),
                new SqlParameter("@Email", email))
            .ToListAsync();

        return result.FirstOrDefault();
    }

    public async Task<int> CreateUserAsync(string username, string email, string passwordHash, int companyId)
    {
        var result = await iSharedDbContext.Database
            .SqlQueryRaw<int>(
                "EXEC Usp_Create_User @Username, @Email, @PasswordHash, @CompanyId",
                new SqlParameter("@Username", username),
                new SqlParameter("@Email", email),
                new SqlParameter("@PasswordHash", passwordHash),
                new SqlParameter("@CompanyId", companyId))
            .ToListAsync();

        return result.FirstOrDefault();
    }

    public async Task<int> CreateCompanyAsync(string companyName)
    {
        var result = await iSharedDbContext.Database
            .SqlQueryRaw<int>(
                "EXEC Usp_Create_Company @CompanyName",
                new SqlParameter("@CompanyName", companyName))
            .ToListAsync();

        return result.FirstOrDefault(); // new CompanyId
    }
}