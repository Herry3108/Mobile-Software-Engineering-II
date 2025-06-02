using BoardGamerApp.ViewModels;

namespace BoardGamerApp.Views;

public partial class AddRatingPage : ContentPage
{
    public AddRatingPage(RatingsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private void OnHostRatingClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var rating = int.Parse((string)button.CommandParameter);
        var viewModel = (RatingsViewModel)BindingContext;
        viewModel.HostRating = rating;
    }

    private void OnFoodRatingClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var rating = int.Parse((string)button.CommandParameter);
        var viewModel = (RatingsViewModel)BindingContext;
        viewModel.FoodRating = rating;
    }

    private void OnOverallRatingClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var rating = int.Parse((string)button.CommandParameter);
        var viewModel = (RatingsViewModel)BindingContext;
        viewModel.OverallRating = rating;
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        var viewModel = (RatingsViewModel)BindingContext;

        viewModel.AddRating();
        await Navigation.PopModalAsync();
    }
}