using BookMyShow.DTO;
namespace BookMyShow.Repository.Interfaces
{
    public interface IAuthRepository
    {
        Task<UserDto> SignupAsync(SignupDto signUpData);
        Task<string> Logout(HttpContext context);
        Task<UserDto> LoginAsync(LoginDto loginData, HttpContext httpContext);
        Task<bool>EmailExistsAsync(string email);
        Task SaveChangesAsync();
        Task<UserDto> Authenticate(LoginDto loginData);
        string GenerateToken(UserDto user);
    }
}