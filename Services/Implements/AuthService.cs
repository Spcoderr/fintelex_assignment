using Fintelex_Assignment.Dtos;
using Fintelex_Assignment.Entities;
using Fintelex_Assignment.Repositories.Interfaces;
using Fintelex_Assignment.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Fintelex_Assignment.Services.Implements
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }

        public async Task<string> RegisterAsync(RegisterDto dto)
        {
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                Role = dto.Role
            };

            var created = await _userRepository.CreateUserAsync(user, dto.Password);
            if (!created) return "User creation failed.";

            var roleAdded = await _userRepository.AddUserToRoleAsync(user, dto.Role);
            return roleAdded ? "User registered successfully" : "Role assignment failed.";
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetUserByEmailAsync(dto.Email);
            if (user == null || !await _userRepository.CheckPasswordAsync(user, dto.Password))
                return "Invalid credentials";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = GenerateJwt(claims);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private JwtSecurityToken GenerateJwt(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: creds
            );
        }
    }
}
