using BoardGamerApp.Business.Models;
using BoardGamerApp.Business.Repositories;

namespace BoardGamerApp.Business.Services;
public class GameService
{
    private readonly IGameRepository _gameRepository;

    public GameService(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public int GetLastId()
    {
        return _gameRepository.GetLastId();
    }

    public Game AddGame(Game game)
    {
        return _gameRepository.AddGame(game);
    }
}
