using simpleAPI.Repositories;
using simpleAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using BCrypt.Net;

namespace simpleAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordService _passwordService;

        public UserService(IUserRepository userRepository, PasswordService passwordService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task AddUserAsync(User user, string plainPassword)
        {
            // Hash the password before storing it
            user.Password = _passwordService.HashPassword(plainPassword);
            await _userRepository.AddUserAsync(user);
        }

        public async Task UpdateUserAsync(User user, string plainPassword)
        {
            if (!string.IsNullOrEmpty(plainPassword))
            {
                // Hash the password before updating
                user.Password = _passwordService.HashPassword(plainPassword);
            }
            await _userRepository.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteUserAsync(id);
        }
    }
}
