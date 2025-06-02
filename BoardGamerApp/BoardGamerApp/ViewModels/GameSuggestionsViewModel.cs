using BoardGamerApp.Business.Models;
using BoardGamerApp.Business.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BoardGamerApp.ViewModels;

public class GameSuggestionsViewModel : BaseViewModel
{
    public ObservableCollection<GameProposal>? Suggestions { get; private set; }
    public GameSession? NextGameNight { get; private set; }

    private string? _newGameName;
    public string? NewGameName
    {
        get => _newGameName;
        set => SetProperty(ref _newGameName, value);
    }

    public ICommand AddSuggestionCommand { get; }
    public ICommand VoteCommand { get; }

    private readonly GameSessionService gameSessionService;
    private readonly UserService userService;
    private readonly GameProposalService gameProposalService;
    private readonly GameService gameService;

    public GameSuggestionsViewModel(
        GameSessionService gameSessionService,
        UserService userService,
        GameProposalService gameProposalService,
        GameService gameService)
    {
        base.Title = "Spielvorschläge";
        this.gameSessionService = gameSessionService;
        this.userService = userService;
        this.gameProposalService = gameProposalService;
        this.gameService = gameService;

        this.NextGameNight = this.gameSessionService.GetNextGameSession() ?? new GameSession();

        var suggestions = this.gameSessionService.GetGameProposalsBySessionId(this.NextGameNight.GameSessionId) ?? new List<GameProposal>();

        this.Suggestions = new ObservableCollection<GameProposal>(suggestions);

        this.AddSuggestionCommand = new Command(AddGameProposal);
        this.VoteCommand = new Command<GameProposal>(ToggleVote);
    }

    private void AddGameProposal()
    {
        if (string.IsNullOrWhiteSpace(NewGameName))
        {
            return;
        }

        var newGame = new Game()
        {
            GameId = this.gameService.GetLastId() + 1,
            Name = this.NewGameName,
            Description = "Test"
        };
        newGame = this.gameService.AddGame(newGame);

        var newGameProposal = new GameProposal()
        {
            GameId = newGame.GameId,
            GameProposalId = this.gameProposalService.GetLastId() + 1,
            GameSessionId = this.NextGameNight.GameSessionId,
            ProposedByUserId = 1,
        };

        var newGameProposals = this.gameSessionService.AddGameProposal(this.NextGameNight.GameSessionId, newGameProposal);
        if (newGameProposals != null)
        {
            this.Suggestions = new ObservableCollection<GameProposal>(newGameProposals);
            OnPropertyChanged(nameof(Suggestions));
        }

        NewGameName = string.Empty;
    }

    private void ToggleVote(GameProposal gameProposal)
    {
        if (gameProposal is null)
        {
            return;
        }

        if (!this.userService.GetAllUsers().Any())
        {
            return;
        }

        var playerId = this.userService.GetAllUsers().First().UserId;
        if (gameProposal.Votes.Select(suggestion => suggestion.UserId).Contains(playerId))
        {
            gameProposal.Votes = this.gameProposalService.RemoveVote(gameProposal.GameId, playerId);
            var indexGameProposal = this.Suggestions.IndexOf(gameProposal);
            if (indexGameProposal >= 0)
            {
                this.Suggestions.RemoveAt(indexGameProposal);
                this.Suggestions.Add(gameProposal);
            }
            return;
        }

        var newVote = new Vote()
        {
            UserId = playerId,
            IsPositive = true,
        };

        this.gameProposalService.AddVote(gameProposal.GameId, newVote);
        gameProposal.Votes.Add(newVote);
        OnPropertyChanged(nameof(Suggestions));
    }
}