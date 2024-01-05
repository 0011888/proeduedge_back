using Microsoft.EntityFrameworkCore;
using proeduedge.DAL;
using proeduedge.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proeduedge.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDBContext _context;

        public UserRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Users> AddUser(Users user)
        {
            _context.Users.Add(user);
            await SaveAsync();
            return user;
        }

        public async Task<Users> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await SaveAsync();
                return null;
            }
            else
            {
                return null;
            }
        }

        public async Task<Users> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
                    }

        public async Task<IEnumerable<Users>> GetUsers()
        {
            return await Task.FromResult(_context.Users.ToList());
        }

        public async Task<Users> UpdateUser(Users user)
        {
            var userToUpdate = await _context.Users.FindAsync(user.Id);
            if (userToUpdate != null)
            {
                userToUpdate.FirstName = user.FirstName;
                userToUpdate.LastName = user.LastName;
                userToUpdate.AvatarUrl = user.AvatarUrl;
                userToUpdate.Email = user.Email;
                userToUpdate.Password = user.Password;
                userToUpdate.Role = user.Role;

                await SaveAsync();
                return userToUpdate;
            }
            else
            {
                return null; // User not found.
            }
        }
        public async Task<Users> UserLogin(LoginModel login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == login.Email);
            if (user != null && user.Password == login.Password)
            {
                return user; // Successful login
            }
            return null; // User not found or password incorrect
        }


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
