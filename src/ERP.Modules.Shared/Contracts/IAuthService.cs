public interface IAuthService
{
    Task<SignupResponseDto> SignupAsync(SignupRequestDto request);
}
