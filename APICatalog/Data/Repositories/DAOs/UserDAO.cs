using APICatalog.APICatalog.Core.Entities.Models;
using APICatalog.APICataolog.Data.Context;
using APICatalog.Core.Common.Enum;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using static APICatalog.Core.Common.Enum.PublicEnum;

namespace APICatalog.Data.Repositories.DAOs
{
    public class UserDAO
    {
        private readonly AppDbContext _context;
        public UserDAO(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == userId && u.DeletionDate == null);

            return user;
        }

        public async Task<User?> GetUserByLoginAsync(string login)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Login == login && u.DeletionDate == null);
            return user;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email && u.DeletionDate == null);
            return user;
        }

        public async Task<User?> GetOrCreateUserSystemAsync(UserType type, [CallerMemberName] string method="")
        {
            var existingUser = await GetUserByLoginAsync(method);

            if (existingUser != null) return existingUser;

            string typeDescription = type.ToString();

            var newUser = new User
            {
                Login = $"{method}_{typeDescription}",
                Type = type,
                IsAdmin = true
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }

        public async Task<User?> CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> UpdateUserAsync(int userId, User user)
        {
            var existingUser = await GetUserByIdAsync(userId);

            if (existingUser == null) return null;

            existingUser.Login = user.Login;
            existingUser.Password = user.Password;
            existingUser.Email = user.Email;
            existingUser.Phone = user.Phone;
            existingUser.IsActive = user.IsActive;
            existingUser.IsAdmin = user.IsAdmin;
            
            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();

            return existingUser;
        }

        public async Task<bool> RemoveUserAsync(int userId)
        {
            var user = await GetUserByIdAsync(userId);

            if (user == null) return false;

            if (user.DeletionDate != null) return false;

            user.DeletionDate = DateTime.UtcNow;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
