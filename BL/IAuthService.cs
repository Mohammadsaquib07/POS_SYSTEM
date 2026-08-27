public interface IAuthService
{
    Task<SignupResponseDto> SignupAsync(SignupRequestDto request);
}

public class AuthService : IAuthService
{
    private readonly IUsersListRepository _userRepository;
    public AuthService(IUsersListRepository userRepository) => _userRepository = userRepository;

    public async Task<SignupResponseDto> SignupAsync(SignupRequestDto request)
    {
        // server-side validation, never trust the client
        if (string.IsNullOrWhiteSpace(request.Username) ||
            string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Password) ||
            string.IsNullOrWhiteSpace(request.CompanyName))
        {
            return new SignupResponseDto { Success = false, Message = "All fields are required." };
        }

        if (request.Password.Length < 8)
        {
            return new SignupResponseDto { Success = false, Message = "Password must be at least 8 characters." };
        }

        var existingCount = await _userRepository.CheckUserExistsAsync(request.Username, request.Email);
        if (existingCount > 0)
        {
            return new SignupResponseDto { Success = false, Message = "Unable to create account with these details." };
        }

        // NEW: create the company for this client first
        var companyId = await _userRepository.CreateCompanyAsync(request.CompanyName);

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        // CHANGED: pass companyId so the user is tied to their own company
        await _userRepository.CreateUserAsync(request.Username, request.Email, passwordHash, companyId);

        return new SignupResponseDto { Success = true, Message = "Account created successfully." };
    }
}