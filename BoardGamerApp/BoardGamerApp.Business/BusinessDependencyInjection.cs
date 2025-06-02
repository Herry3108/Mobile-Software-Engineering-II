using BoardGamerApp.Business.Services;

namespace BoardGamerApp.Business;

public static class BusinessDependencyInjection
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddTransient<GameProposalService>();
        services.AddTransient<GameSessionService>();
        services.AddTransient<UserService>();
        services.AddTransient<MessageService>();
        services.AddTransient<GameService>();

        return services;
    }
}