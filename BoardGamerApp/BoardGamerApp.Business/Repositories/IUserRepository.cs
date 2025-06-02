using BoardGamerApp.Business.Models;

namespace BoardGamerApp.Business.Repositories;
public interface IUserRepository
{
    User? GetUserById(int userId);
    IEnumerable<User> GetAllUsers();
}
