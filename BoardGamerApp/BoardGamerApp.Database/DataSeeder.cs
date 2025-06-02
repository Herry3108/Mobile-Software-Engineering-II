namespace BoardGamerApp.Database;
using BoardGamerApp.Business.Models;

public static class DataSeeder
{
    public static List<User> GetUsers()
    {
        return new List<User>
        {
            new User {UserId = 1 , Username = "Alice", Email = "alice@example.com" },
            new User {UserId = 2, Username = "Bob", Email = "bob@example.com" },
            new User {UserId = 3, Username = "Charlie", Email = "charlie@example.com" },
            new User {UserId = 4, Username = "Natan", Email = "natan@example.com" }
        };
    }

    public static List<Game> GetGames()
    {
        return new List<Game>
        {
            new Game {GameId = 1, Name = "Chess", Description = "A strategy board game" },
            new Game {GameId = 2, Name = "Monopoly", Description = "A real estate trading game" },
            new Game {GameId = 3, Name = "Siedler", Description = "A strategy game" },
            new Game {GameId = 4, Name = "Catan", Description = "A resource management game" }
        };
    }

    public static List<GameSession> GetGameSessions()
    {
        return new List<GameSession>
        {
            new GameSession {GameSessionId = 1, UserId = 1, DateTime = DateTime.UtcNow.AddDays(1), GameId = 1, Location = "Stuttgart" },
            new GameSession {GameSessionId = 2, UserId = 2, DateTime = DateTime.UtcNow.AddDays(2), GameId = 2, Location = "Berlin"},
            new GameSession {GameSessionId = 3, UserId = 3, DateTime = DateTime.UtcNow.AddDays(3), GameId = 3, Location = "München"},
            new GameSession {GameSessionId = 4, UserId = 4, DateTime = DateTime.UtcNow.AddDays(4), GameId = 4, Location = "Frankfurt"}
        };
    }

    public static List<GameProposal> GetGameProposals()
    {
        return new List<GameProposal>
        {
            new GameProposal {GameProposalId = 1, GameId = 1, GameSessionId = 1, ProposedByUserId = 1},
            new GameProposal {GameProposalId = 2, GameId = 2, GameSessionId = 2, ProposedByUserId = 2},
            new GameProposal {GameProposalId = 3, GameId = 3, GameSessionId = 3, ProposedByUserId = 3},
            new GameProposal {GameProposalId = 4, GameId = 1, GameSessionId = 4, ProposedByUserId = 1 },
            new GameProposal {GameProposalId = 5, GameId = 2, GameSessionId = 5, ProposedByUserId = 4},
            new GameProposal {GameProposalId = 6, GameId = 2, GameSessionId = 6, ProposedByUserId = 2},
            new GameProposal {GameProposalId = 7, GameId = 1, GameSessionId = 1, ProposedByUserId = 1},
            new GameProposal {GameProposalId = 8, GameId = 4, GameSessionId = 8, ProposedByUserId = 4 },
            new GameProposal {GameProposalId = 9, GameId = 3, GameSessionId = 1, ProposedByUserId = 1},
            new GameProposal {GameProposalId = 10, GameId = 2, GameSessionId = 10, ProposedByUserId = 2},
            new GameProposal {GameProposalId = 11, GameId = 1, GameSessionId = 11, ProposedByUserId = 3},
            new GameProposal {GameProposalId = 12, GameId = 1, GameSessionId = 12, ProposedByUserId = 4 }
        };
    }

    public static List<Vote> GetVotes()
    {
        return new List<Vote>
        {
            new Vote {VoteId = 1, UserId = 1, GameProposalId = 1, IsPositive = true},
            new Vote {VoteId = 2, UserId = 2, GameProposalId = 2, IsPositive = true}
        };
    }

    public static List<Message> GetMessages()
    {
        return new List<Message>
        {
            new Message {MessageId = 1, SenderId = 1, GameSessionId = 1,Content = "Hello, everyone!" },
            new Message {MessageId = 2, SenderId = 2, GameSessionId = 2, Content = "Hi, Alice!" }
        };
    }
}
