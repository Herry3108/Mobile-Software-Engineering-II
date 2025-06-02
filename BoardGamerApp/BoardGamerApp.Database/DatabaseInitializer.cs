using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;

namespace BoardGamerApp.Database;

public static class FirebaseInitializer
{
    private static readonly IFirebaseConfig config = new FirebaseConfig
    {
        AuthSecret = "ToDo",
        BasePath = "ToDo"
    };

    private static readonly IFirebaseClient client = new FirebaseClient(config);

    public static async Task InitializeAsync()
    {
        var response = await client.GetAsync("users");

        bool isDatabaseEmpty = response == null || response.Body == "null";
        if (!isDatabaseEmpty)
        {
            return;
        }

        await SeedUsersAsync();
        await SeedGamesAsync();
        await SeedGameSessionsAsync();
        await SeedGameProposalsAsync();
        await SeedVotesAsync();
        await SeedMessagesAsync();
    }

    private static async Task SeedUsersAsync()
    {
        var users = DataSeeder.GetUsers();
        foreach (var user in users)
        {
            await client.SetAsync($"users/{user.UserId}", user);
        }
    }

    private static async Task SeedGamesAsync()
    {
        var games = DataSeeder.GetGames();
        foreach (var game in games)
        {
            await client.SetAsync($"games/{game.GameId}", game);
        }
    }

    private static async Task SeedGameSessionsAsync()
    {
        var gameSessions = DataSeeder.GetGameSessions();
        foreach (var session in gameSessions)
        {
            await client.SetAsync($"gameSessions/{session.GameSessionId}", session);
        }
    }

    private static async Task SeedGameProposalsAsync()
    {
        var gameProposals = DataSeeder.GetGameProposals();
        foreach (var proposal in gameProposals)
        {
            await client.SetAsync($"gameProposals/{proposal.GameProposalId}", proposal);
        }
    }

    private static async Task SeedVotesAsync()
    {
        var votes = DataSeeder.GetVotes();
        foreach (var vote in votes)
        {
            await client.SetAsync($"votes/{vote.VoteId}", vote);
        }
    }

    private static async Task SeedMessagesAsync()
    {
        var messages = DataSeeder.GetMessages();
        foreach (var message in messages)
        {
            await client.SetAsync($"messages/{message.MessageId}", message);
        }
    }
}