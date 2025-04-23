using Fintelex_Assignment.Entities;

namespace Fintelex_Assignment.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> GetUserByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task<List<string>> GetUserRolesAsync(ApplicationUser user);
        Task<bool> CreateUserAsync(ApplicationUser user, string password);
        Task<bool> AddUserToRoleAsync(ApplicationUser user, string role);
    }
}
