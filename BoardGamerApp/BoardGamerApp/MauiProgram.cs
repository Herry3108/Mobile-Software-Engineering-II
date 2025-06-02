using BoardGamerApp.Business;
using BoardGamerApp.Database;
using BoardGamerApp.ViewModels;
using BoardGamerApp.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;

namespace BoardGamerApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .RegisterFirebase(); // Firebase initialisieren

        // Register ViewModels
        builder.Services.AddTransient<HomeViewModel>();
        builder.Services.AddTransient<GameSuggestionsViewModel>();
        builder.Services.AddSingleton<RatingsViewModel>();
        builder.Services.AddTransient<MessagesViewModel>();
        builder.Services.AddTransient<ProfileViewModel>();

        // Register Views
        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<GameSuggestionsPage>();
        builder.Services.AddTransient<RatingsPage>();
        builder.Services.AddTransient<MessagesPage>();
        builder.Services.AddTransient<ProfilePage>();
        builder.Services.AddTransient<AddRatingPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        //Add Database and Business services
        IConfiguration configuration = builder.Configuration;
        builder.Services.AddDatabaseRepositories(configuration);
        builder.Services.AddBusinessServices();

        // Build the app
        var app = builder.Build();

        return app;
    }

    // Firebase-Initialisierungsmethode
    private static MauiAppBuilder RegisterFirebase(this MauiAppBuilder builder)
    {
        builder.ConfigureLifecycleEvents(events =>
        {
#if ANDROID
            events.AddAndroid(android => android.OnCreate((activity, bundle) => {
                Firebase.FirebaseApp.InitializeApp(activity);
            }));
#endif
        });

        return builder;
    }
}