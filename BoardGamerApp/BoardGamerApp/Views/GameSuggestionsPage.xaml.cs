using BoardGamerApp.ViewModels;

namespace BoardGamerApp.Views;

public partial class GameSuggestionsPage : ContentPage
{
    public GameSuggestionsPage(GameSuggestionsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private async void OnAddSuggestionClicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Neues Spiel vorschlagen", "Spielname:", "Vorschlagen", "Abbrechen");
        if (!string.IsNullOrWhiteSpace(result))
        {
            var viewModel = (GameSuggestionsViewModel)BindingContext;
            viewModel.NewGameName = result;
            viewModel.AddSuggestionCommand.Execute(null);
        }
    }
}