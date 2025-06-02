using BoardGamerApp.Business.Models;
using BoardGamerApp.Business.Repositories;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;

namespace BoardGamerApp.Database;
public class UserRepository : IUserRepository
{
    private readonly IFirebaseClient _client;

    public UserRepository()
    {
        var config = new FirebaseConfig
        {
            AuthSecret = "ToDo",
            BasePath = "ToDo"
        };
        _client = new FirebaseClient(config);
    }

    public IEnumerable<User> GetAllUsers()
    {
        var response = _client.Get("users");
        if (response == null || string.IsNullOrEmpty(response.Body) || response.Body == "null")
        {
            return new List<User>();
        }

        var users = FirebaseParser.ParseCollection<User>(response.Body);
        return users;
    }

    public User? GetUserById(int userId)
    {
        var response = _client.Get($"users/{userId}");
        if (response == null || string.IsNullOrEmpty(response.Body) || response.Body == "null")
        {
            return null;
        }

        var user = FirebaseParser.ParseSingle<User>(response.Body);
        return user;
    }
}