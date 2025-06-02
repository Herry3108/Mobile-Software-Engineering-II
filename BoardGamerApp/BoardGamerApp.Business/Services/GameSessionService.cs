using BoardGamerApp.Business.Models;
using BoardGamerApp.Business.Repositories;

namespace BoardGamerApp.Business.Services;
public class GameSessionService(IGameSessionRepository gameSessionRepository)
{
    public GameSession? GetGameSessionById(int gameSessionId)
    {
        return gameSessionRepository.GetGameSessionById(gameSessionId);
    }

    public IEnumerable<GameSession> GetAllGameSessions()
    {
        return gameSessionRepository.GetAllGameSessions();
    }

    public IEnumerable<GameSession> AddGameSession(GameSession gameSession)
    {
        return gameSessionRepository.AddGameSession(gameSession);
    }

    public IEnumerable<GameSession> UpdateGameSession(GameSession gameSession)
    {
        return gameSessionRepository.UpdateGameSession(gameSession);
    }

    public IEnumerable<GameSession> DeleteGameSessionAsync(int gameSessionId)
    {
        return gameSessionRepository.DeleteGameSession(gameSessionId);
    }

    public GameSession? GetNextGameSession()
    {
        return gameSessionRepository.GetNextGameSession();
    }

    public IEnumerable<GameProposal> AddGameProposal(int gameSessionId, GameProposal gameProposal)
    {
        return gameSessionRepository.AddGameProposal(gameSessionId, gameProposal);
    }

    public IEnumerable<GameProposal> RemoveGameProposal(int gameSessionId, GameProposal gameProposal)
    {
        return gameSessionRepository.RemoveGameProposal(gameSessionId, gameProposal);
    }

    public IEnumerable<Rating> AddRating(int gameSessionId, Rating rating)
    {
        return gameSessionRepository.AddRating(gameSessionId, rating);
    }

    public IEnumerable<Rating> RemoveRating(int gameSessionId, int ratingId)
    {
        return gameSessionRepository.RemoveRating(gameSessionId, ratingId);
    }

    public IEnumerable<GameProposal> GetGameProposalsBySessionId(int gameSessionId)
    {
        return gameSessionRepository.GetGameProposalsBySessionId(gameSessionId);
    }

    public IEnumerable<Rating> GetRatingsBySessionId(int gameSessionId)
    {
        return gameSessionRepository.GetRatingsBySessionId(gameSessionId);
    }

    public int GetLastId()
    {
        var allSessions = gameSessionRepository.GetAllGameSessions();
        if (!allSessions.Any())
        {
            return 0;
        }
        return allSessions.Max(s => s.GameSessionId);
    }
}
