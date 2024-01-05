using proeduedge.Models;

namespace proeduedge.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<Users>> GetUsers();
        Task<Users> GetUser(int id);
        Task<Users> AddUser(Users user);
        Task<Users> UpdateUser(Users user);
        Task<Users> DeleteUser(int id);
        Task<Users> UserLogin(LoginModel login);
    }
}
