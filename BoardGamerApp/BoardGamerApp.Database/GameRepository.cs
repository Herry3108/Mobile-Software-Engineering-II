using BoardGamerApp.Business.Models;
using BoardGamerApp.Business.Repositories;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;

namespace BoardGamerApp.Database;
public class GameRepository : IGameRepository
{
    private readonly IFirebaseClient _client;

    public GameRepository()
    {
        var config = new FirebaseConfig
        {
            AuthSecret = "ToDo",
            BasePath = "ToDo"
        };
        _client = new FirebaseClient(config);
    }

    public int GetLastId()
    {
        var gamesResponse = _client.Get("games");
        if (gamesResponse == null || string.IsNullOrEmpty(gamesResponse.Body) || gamesResponse.Body == "null")
        {
            return 0;
        }

        var games = FirebaseParser.ParseCollection<Game>(gamesResponse.Body);
        if (!games.Any())
        {
            return 0;
        }

        return games.Max(g => g.GameId);
    }

    public Game AddGame(Game game)
    {
        _client.Set($"games/{game.GameId}", game);
        return game;
    }
}