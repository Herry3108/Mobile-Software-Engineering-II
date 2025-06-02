using BoardGamerApp.Business.Models;
using BoardGamerApp.Business.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BoardGamerApp.ViewModels;

public class HomeViewModel : BaseViewModel
{
    public ObservableCollection<GameSession>? UpcomingGameNights { get; private set; }
    public GameSession? NextGameNight { get; private set; }

    private bool _isAddingGameSession = false;
    public bool IsAddingGameSession
    {
        get => _isAddingGameSession;
        set => SetProperty(ref _isAddingGameSession, value);
    }

    private DateTime _newSessionDateTime = DateTime.Now.AddDays(7);
    public DateTime NewSessionDateTime
    {
        get => _newSessionDateTime;
        set => SetProperty(ref _newSessionDateTime, value);
    }

    private TimeSpan _newSessionTime = new TimeSpan(19, 0, 0);
    public TimeSpan NewSessionTime
    {
        get => _newSessionTime;
        set => SetProperty(ref _newSessionTime, value);
    }

    private string? _newSessionLocation;
    public string? NewSessionLocation
    {
        get => _newSessionLocation;
        set => SetProperty(ref _newSessionLocation, value);
    }

    private string? _newSessionNotes;
    public string? NewSessionNotes
    {
        get => _newSessionNotes;
        set => SetProperty(ref _newSessionNotes, value);
    }

    public ICommand ShowAddGameSessionCommand { get; }
    public ICommand AddGameSessionCommand { get; }
    public ICommand CancelAddGameSessionCommand { get; }

    private readonly GameSessionService gameSessionService;
    private readonly UserService userService;

    public HomeViewModel(
        GameSessionService gameSessionService,
        UserService userService)
    {
        base.Title = "Home";
        this.gameSessionService = gameSessionService;
        this.userService = userService;

        LoadGameSessions();

        this.ShowAddGameSessionCommand = new Command(ShowAddGameSession);
        this.AddGameSessionCommand = new Command(AddGameSession);
        this.CancelAddGameSessionCommand = new Command(CancelAddGameSession);
    }

    private void ShowAddGameSession()
    {
        IsAddingGameSession = true;
        NewSessionDateTime = DateTime.Now.AddDays(7);
        NewSessionTime = new TimeSpan(19, 0, 0);
        NewSessionLocation = string.Empty;
        NewSessionNotes = string.Empty;
    }

    private void CancelAddGameSession()
    {
        IsAddingGameSession = false;
        NewSessionLocation = string.Empty;
        NewSessionNotes = string.Empty;
    }

    private void AddGameSession()
    {
        if (string.IsNullOrWhiteSpace(NewSessionLocation))
        {
            return;
        }

        var sessionDateTime = NewSessionDateTime.Date + NewSessionTime;

        if (sessionDateTime <= DateTime.Now)
        {
            return;
        }

        var defaultGameId = 1;

        var randomUser = GetRandomUser(this.NextGameNight.User);

        var newGameSession = new GameSession()
        {
            GameSessionId = this.gameSessionService.GetLastId() + 1,
            DateTime = sessionDateTime,
            UserId = randomUser.UserId,
            Location = NewSessionLocation,
            Notes = NewSessionNotes ?? string.Empty,
            IsFinished = false,
            GameId = defaultGameId
        };

        var updatedSessions = this.gameSessionService.AddGameSession(newGameSession);

        if (updatedSessions != null)
        {
            LoadGameSessions();
            IsAddingGameSession = false;
            CancelAddGameSession();
        }
    }

    private User? GetRandomUser(User currentUser)
    {
        var users = this.userService.GetAllUsers()
            .Where(u => u.UserId != 1)
            .ToList();

        if (!users.Any())
        {
            return null;
        }

        var random = new Random();
        int index = random.Next(users.Count);
        return users[index];
    }
    private void LoadGameSessions()
    {
        this.NextGameNight = this.gameSessionService.GetNextGameSession();
        OnPropertyChanged(nameof(NextGameNight));

        var allSessions = this.gameSessionService.GetAllGameSessions()
            .Where(s => s.DateTime > DateTime.Now && !s.IsFinished)
            .OrderBy(s => s.DateTime)
            .ToList();

        if (NextGameNight != null)
        {
            allSessions = allSessions.Where(s => s.GameSessionId != NextGameNight.GameSessionId).ToList();
        }

        this.UpcomingGameNights = new ObservableCollection<GameSession>(allSessions);
        OnPropertyChanged(nameof(UpcomingGameNights));
    }

    public void RefreshData()
    {
        LoadGameSessions();
    }
}