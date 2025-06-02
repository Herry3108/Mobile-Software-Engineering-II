using BoardGamerApp.Business.Models;
using BoardGamerApp.Business.Repositories;

namespace BoardGamerApp.Business.Services;
public class UserService(IUserRepository userRepository)
{
    public User? GetUserById(int userId)
    {
        return userRepository.GetUserById(userId);
    }

    public IEnumerable<User> GetAllUsers()
    {
        return userRepository.GetAllUsers();
    }
}
