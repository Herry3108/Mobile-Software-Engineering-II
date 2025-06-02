using BoardGamerApp.Business.Models;

namespace BoardGamerApp.Business.Repositories;
public interface IGameSessionRepository
{
    GameSession? GetGameSessionById(int gameSessionId);
    IEnumerable<GameSession> GetAllGameSessions();
    IEnumerable<GameSession> UpdateGameSession(GameSession gameSession);
    IEnumerable<GameSession> DeleteGameSession(int gameSessionId);
    GameSession? GetNextGameSession();
    IEnumerable<GameProposal> AddGameProposal(int gameSessionId, GameProposal gameProposal);
    IEnumerable<GameSession> AddGameSession(GameSession gameSession);
    IEnumerable<GameProposal> RemoveGameProposal(int gameSessionId, GameProposal gameProposal);
    IEnumerable<Rating> AddRating(int gameSessionId, Rating rating);
    IEnumerable<Rating> RemoveRating(int gameSessionId, int ratingId);
    IEnumerable<GameProposal> GetGameProposalsBySessionId(int gameSessionId);
    IEnumerable<Rating> GetRatingsBySessionId(int gameSessionId);
}
