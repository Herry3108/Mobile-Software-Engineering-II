using BoardGamerApp.ViewModels;

namespace BoardGamerApp.Views;

public partial class MessagesPage : ContentPage
{
    public MessagesPage(MessagesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (messagesCollection.ItemsSource != null)
        {
            var count = ((System.Collections.ICollection)messagesCollection.ItemsSource).Count;
            if (count > 0)
            {
                messagesCollection.ScrollTo(count - 1);
            }
        }
    }
}