using BoardGamerApp.Business.Repositories;
using Microsoft.Extensions.Configuration;

namespace BoardGamerApp.Database;

public static class DBDependencyInjection
{
    public static IServiceCollection AddDatabaseRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IGameSessionRepository, GameSessionRepository>();
        services.AddTransient<IGameProposalRepository, GameProposalRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IMessageRepository, MessageRepository>();
        services.AddTransient<IGameRepository, GameRepository>();

        //Seed test data
        Task.Run(async () => await FirebaseInitializer.InitializeAsync());

        return services;
    }
}

