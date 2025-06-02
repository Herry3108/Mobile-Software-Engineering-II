using BoardGamerApp.ViewModels;

namespace BoardGamerApp.Views;

public partial class RatingsPage : ContentPage
{
    private readonly IServiceProvider _serviceProvider;

    public RatingsPage(RatingsViewModel viewModel, IServiceProvider serviceProvider)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _serviceProvider = serviceProvider;
    }

    private async void OnAddRatingClicked(object sender, EventArgs e)
    {
        var viewModel = (RatingsViewModel)BindingContext;
        var addRatingPage = new AddRatingPage(viewModel);

        await Navigation.PushModalAsync(addRatingPage);
    }
}