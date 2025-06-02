using BoardGamerApp.Business.Models;
using BoardGamerApp.Business.Repositories;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;

namespace BoardGamerApp.Database;
public class GameSessionRepository : IGameSessionRepository
{
    private readonly IFirebaseClient _client;

    public GameSessionRepository()
    {
        var config = new FirebaseConfig
        {
            AuthSecret = "ToDo",
            BasePath = "ToDo"
        };
        _client = new FirebaseClient(config);
    }

    public IEnumerable<GameSession> AddGameSession(GameSession gameSession)
    {
        var response = _client.Set($"gameSessions/{gameSession.GameSessionId}", gameSession);
        _client.Set($"user-gameSessions/{gameSession.UserId}/{gameSession.GameSessionId}", true);

        return GetAllGameSessions();
    }

    public IEnumerable<GameSession> DeleteGameSession(int gameSessionId)
    {
        var gameSession = GetGameSessionById(gameSessionId);
        if (gameSession == null)
        {
            return new List<GameSession>();
        }

        _client.Delete($"gameSessions/{gameSessionId}");
        _client.Delete($"user-gameSessions/{gameSession.UserId}/{gameSessionId}");

        return GetAllGameSessions();
    }

    public IEnumerable<GameSession> GetAllGameSessions()
    {
        var response = _client.Get("gameSessions");
        if (response == null)
        {
            return new List<GameSession>();
        }

        if (string.IsNullOrEmpty(response.Body) || response.Body == "null")
        {
            return new List<GameSession>();
        }

        var sessions = FirebaseParser.ParseCollection<GameSession>(response.Body);
        var sessionsList = sessions.ToList();

        foreach (var session in sessionsList)
        {
            var gameResponse = _client.Get($"games/{session.GameId}");
            if (gameResponse?.Body != null && gameResponse.Body != "null")
            {
                session.Game = FirebaseParser.ParseSingle<Game>(gameResponse.Body);
            }
            var userResponse = _client.Get($"users/{session.UserId}");
            if (userResponse?.Body != null && userResponse.Body != "null")
            {
                session.User = FirebaseParser.ParseSingle<User>(userResponse.Body);
            }
        }

        return sessionsList;
    }

    public GameSession? GetGameSessionById(int gameSessionId)
    {
        var response = _client.Get($"gameSessions/{gameSessionId}");
        if (response.Body == "null")
        {
            return null;
        }

        var session = FirebaseParser.ParseSingle<GameSession>(response.Body);

        var userResponse = _client.Get($"users/{session.UserId}");
        if (userResponse.Body != "null")
        {
            session.User = FirebaseParser.ParseSingle<User>(userResponse.Body);
        }

        return session;
    }

    public IEnumerable<GameSession> UpdateGameSession(GameSession gameSession)
    {
        _client.Update($"gameSessions/{gameSession.GameSessionId}", gameSession);
        return GetAllGameSessions();
    }

    public GameSession? GetNextGameSession()
    {
        var sessions = GetAllGameSessions();
        if (!sessions.Any())
        {
            return null;
        }

        return sessions.MinBy(x => x.DateTime);
    }

    public IEnumerable<GameProposal> AddGameProposal(int gameSessionId, GameProposal gameProposal)
    {
        var gameSession = GetGameSessionById(gameSessionId);
        if (gameSession == null)
        {
            return new List<GameProposal>();
        }

        var gameResponse = _client.Get($"games/{gameProposal.GameId}");
        if (gameResponse.Body == "null")
        {
            return new List<GameProposal>();
        }

        gameProposal.GameSessionId = gameSessionId;
        gameProposal.ProposedAt = DateTime.Now;

        var response = _client.Set($"gameProposals/{gameProposal.GameProposalId}", gameProposal);

        return GetGameProposalsBySessionId(gameSessionId);
    }

    public IEnumerable<GameProposal> RemoveGameProposal(int gameSessionId, GameProposal gameProposal)
    {
        _client.Delete($"gameProposals/{gameProposal.GameProposalId}");
        return GetGameProposalsBySessionId(gameSessionId);
    }

    public IEnumerable<Rating> AddRating(int gameSessionId, Rating rating)
    {
        _client.Set($"ratings/{rating.RatingId}", rating);
        return GetRatingsBySessionId(gameSessionId);
    }

    public IEnumerable<Rating> RemoveRating(int gameSessionId, int ratingId)
    {
        _client.Delete($"ratings/{ratingId}");
        return GetRatingsBySessionId(gameSessionId);
    }

    public IEnumerable<GameProposal> GetGameProposalsBySessionId(int gameSessionId)
    {
        var response = _client.Get("gameProposals");
        var allProposals = FirebaseParser.ParseCollection<GameProposal>(response.Body);

        var sessionProposals = allProposals.Where(p => p.GameSessionId == gameSessionId).ToList();
        foreach (var proposal in sessionProposals)
        {
            var gameResponse = _client.Get($"games/{proposal.GameId}");
            if (gameResponse?.Body != null && gameResponse.Body != "null")
            {
                proposal.Game = FirebaseParser.ParseSingle<Game>(gameResponse.Body);
            }
        }

        return sessionProposals;
    }

    public IEnumerable<Rating> GetRatingsBySessionId(int gameSessionId)
    {
        var response = _client.Get("ratings");
        if (response == null || string.IsNullOrEmpty(response.Body) || response.Body == "null")
        {
            return new List<Rating>();
        }

        var allRatings = FirebaseParser.ParseCollection<Rating>(response.Body);
        var sessionRatings = allRatings.Where(r => r.GameSessionId == gameSessionId).ToList();

        return sessionRatings;
    }
}