using Fintelex_Assignment.Entities;
using Fintelex_Assignment.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Fintelex_Assignment.Repositories.Implements
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<List<string>> GetUserRolesAsync(ApplicationUser user)
        {
            return (await _userManager.GetRolesAsync(user)).ToList();
        }

        public async Task<bool> CreateUserAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }

        public async Task<bool> AddUserToRoleAsync(ApplicationUser user, string role)
        {
            var result = await _userManager.AddToRoleAsync(user, role);
            return result.Succeeded;
        }
    }
}
