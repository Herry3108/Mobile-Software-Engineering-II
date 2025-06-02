using BoardGamerApp.Business.Models;
using BoardGamerApp.Business.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BoardGamerApp.ViewModels;

public class RatingViewModel
{
    public string RaterInitials { get; set; } = string.Empty;
    public string RaterName { get; set; } = string.Empty;
    public string HostRating { get; set; } = string.Empty;
    public string FoodRating { get; set; } = string.Empty;
    public string OverallRating { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public bool HasComment => !string.IsNullOrWhiteSpace(Comment);
}

public class RatingsViewModel : BaseViewModel
{
    private ObservableCollection<RatingViewModel> _ratings;
    public ObservableCollection<RatingViewModel> Ratings
    {
        get => _ratings;
        private set => SetProperty(ref _ratings, value);
    }

    private int _hostRating = 3;
    public int HostRating
    {
        get => _hostRating;
        set
        {
            if (SetProperty(ref _hostRating, value))
            {
                UpdateRatingColors();
            }
        }
    }

    private int _foodRating = 3;
    public int FoodRating
    {
        get => _foodRating;
        set
        {
            if (SetProperty(ref _foodRating, value))
            {
                UpdateRatingColors();
            }
        }
    }

    private int _overallRating = 3;
    public int OverallRating
    {
        get => _overallRating;
        set
        {
            if (SetProperty(ref _overallRating, value))
            {
                UpdateRatingColors();
            }
        }
    }

    private string _comment = string.Empty;
    public string Comment
    {
        get => _comment;
        set => SetProperty(ref _comment, value);
    }

    // Host rating color properties
    public Color IsHostRating1 => HostRating == 1 ? Colors.Orange : Colors.LightGray;
    public Color IsHostRating2 => HostRating == 2 ? Colors.Orange : Colors.LightGray;
    public Color IsHostRating3 => HostRating == 3 ? Colors.Orange : Colors.LightGray;
    public Color IsHostRating4 => HostRating == 4 ? Colors.Orange : Colors.LightGray;
    public Color IsHostRating5 => HostRating == 5 ? Colors.Orange : Colors.LightGray;

    // Food rating color properties
    public Color IsFoodRating1 => FoodRating == 1 ? Colors.Orange : Colors.LightGray;
    public Color IsFoodRating2 => FoodRating == 2 ? Colors.Orange : Colors.LightGray;
    public Color IsFoodRating3 => FoodRating == 3 ? Colors.Orange : Colors.LightGray;
    public Color IsFoodRating4 => FoodRating == 4 ? Colors.Orange : Colors.LightGray;
    public Color IsFoodRating5 => FoodRating == 5 ? Colors.Orange : Colors.LightGray;

    // Overall rating color properties
    public Color IsOverallRating1 => OverallRating == 1 ? Colors.Orange : Colors.LightGray;
    public Color IsOverallRating2 => OverallRating == 2 ? Colors.Orange : Colors.LightGray;
    public Color IsOverallRating3 => OverallRating == 3 ? Colors.Orange : Colors.LightGray;
    public Color IsOverallRating4 => OverallRating == 4 ? Colors.Orange : Colors.LightGray;
    public Color IsOverallRating5 => OverallRating == 5 ? Colors.Orange : Colors.LightGray;

    public ICommand AddRatingCommand { get; }
    private readonly GameSessionService gameSessionService;
    private readonly UserService userService;

    public RatingsViewModel(GameSessionService gameSessionService, UserService userService)
    {
        try
        {
            Title = "Bewertungen";
            this.gameSessionService = gameSessionService ?? throw new ArgumentNullException(nameof(gameSessionService));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));

            _ratings = new ObservableCollection<RatingViewModel>();

            AddRatingCommand = new Command(AddRating);

            LoadRatings();
            UpdateRatingColors();
        }
        catch (Exception ex)
        {
            _ratings = new ObservableCollection<RatingViewModel>();
            AddRatingCommand = new Command(() => { });
        }
    }

    private void LoadRatings()
    {
        _ratings.Clear();

        var gameSessions = gameSessionService.GetAllGameSessions();
        if (gameSessions == null)
        {
            return;
        }

        int totalRatings = 0;

        foreach (var session in gameSessions)
        {
            var currentRating = gameSessionService.GetRatingsBySessionId(session.GameSessionId);
            if (!currentRating.Any())
            {
                continue;
            }
            foreach (var rating in currentRating)
            {
                var rater = userService.GetUserById(rating.RatedByUserId);
                string raterName = rater?.Username ?? "Unknown";

                string initials = "?";
                if (!string.IsNullOrEmpty(rater?.Username))
                {
                    initials = rater.Username.Substring(0, 1);
                }

                var ratingViewModel = new RatingViewModel
                {
                    RaterInitials = initials,
                    RaterName = raterName,
                    HostRating = rating.HostRating,
                    FoodRating = rating.FoodRating,
                    OverallRating = rating.OverallRating,
                    Comment = rating.Comment
                };

                _ratings.Add(ratingViewModel);
                totalRatings++;
            }
        }

        OnPropertyChanged(nameof(Ratings));
    }

    private void UpdateRatingColors()
    {
        // Notify all rating color properties
        OnPropertyChanged(nameof(IsHostRating1));
        OnPropertyChanged(nameof(IsHostRating2));
        OnPropertyChanged(nameof(IsHostRating3));
        OnPropertyChanged(nameof(IsHostRating4));
        OnPropertyChanged(nameof(IsHostRating5));

        OnPropertyChanged(nameof(IsFoodRating1));
        OnPropertyChanged(nameof(IsFoodRating2));
        OnPropertyChanged(nameof(IsFoodRating3));
        OnPropertyChanged(nameof(IsFoodRating4));
        OnPropertyChanged(nameof(IsFoodRating5));

        OnPropertyChanged(nameof(IsOverallRating1));
        OnPropertyChanged(nameof(IsOverallRating2));
        OnPropertyChanged(nameof(IsOverallRating3));
        OnPropertyChanged(nameof(IsOverallRating4));
        OnPropertyChanged(nameof(IsOverallRating5));
    }
    public void AddRating()
    {
        var currentUser = userService.GetAllUsers().FirstOrDefault();
        if (currentUser == null)
        {
            return;
        }

        int userId = currentUser.UserId;

        var nextGameSession = gameSessionService.GetNextGameSession();
        if (nextGameSession == null)
        {
            return;
        }

        int gameSessionId = nextGameSession.GameSessionId;

        var newRating = new Rating()
        {
            GameSessionId = gameSessionId,
            RatedByUserId = userId,
            FoodRating = FoodRating.ToString(),
            HostRating = HostRating.ToString(),
            OverallRating = OverallRating.ToString(),
            Comment = Comment
        };


        var ratings = gameSessionService.AddRating(gameSessionId, newRating);

        LoadRatings();

        // Reset form
        HostRating = 3;
        FoodRating = 3;
        OverallRating = 3;
        Comment = string.Empty;

        // Force UI update
        MainThread.BeginInvokeOnMainThread(() =>
        {
            OnPropertyChanged(nameof(Ratings));
        });
    }
}