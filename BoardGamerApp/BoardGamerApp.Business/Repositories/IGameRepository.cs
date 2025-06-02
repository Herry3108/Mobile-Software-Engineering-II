using BoardGamerApp.Business.Models;

namespace BoardGamerApp.Business.Repositories;
public interface IGameRepository
{
    int GetLastId();

    Game AddGame(Game game);
}
