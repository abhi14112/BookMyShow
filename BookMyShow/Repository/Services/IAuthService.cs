using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookMyShow.Data;
using BookMyShow.DTO;
using BookMyShow.Models;
using BookMyShow.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
namespace BookMyShow.Repository.Services
{

    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        public AuthRepository(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public async Task<string>Logout(HttpContext httpContext)
        {
            httpContext.Response.Cookies.Delete("jwtToken");
            return "Logout Succefully";
        }
        public async Task<UserDto> LoginAsync(LoginDto userData,HttpContext httpContext)
        {
            var user = await Authenticate(userData);
            if (user != null)
            {
                var token = GenerateToken(user);
                httpContext.Response.Cookies.Append("jwtToken", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddHours(0.1)
                });
                return user;
            }
            else
            {
                return null;
            }
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<UserDto> SignupAsync(SignupDto user)
        {
            var userData = new User
            {
                UserName = user.UserName,
                Email = user.Email,
                Mobile = user.Mobile,
                Password = user.Password,
            };
            _context.Users.Add(userData);
            await _context.SaveChangesAsync();
            _context.SaveChanges();
            return new UserDto
            {
                Id = userData.Id,
                ProfilePic = userData.ProfilePic,
                UserName = user.UserName,
                Email = user.Email,
                Mobile = user.Mobile,
                Role = userData.Role
            };
        }
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(o => o.Email.ToLower() == email.ToLower());
        }
        public async Task<UserDto> Authenticate(LoginDto userData)
        {
            var currentUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == userData.Email.ToLower() &&
                    u.Password == userData.Password);
            if (currentUser ==null)
            {
                return null;
            }

            return new UserDto
            {
                Id = currentUser.Id,
                ProfilePic = currentUser.ProfilePic,
                UserName = currentUser.UserName,
                Email = currentUser.Email,
                Mobile = currentUser.Mobile,
                Role = currentUser.Role
            };
        }
        public string GenerateToken(UserDto user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
            };
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}