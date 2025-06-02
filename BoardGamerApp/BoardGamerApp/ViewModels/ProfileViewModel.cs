using BoardGamerApp.Business.Models;
using BoardGamerApp.Business.Services;

namespace BoardGamerApp.ViewModels;

public class ProfileViewModel : BaseViewModel
{
    private readonly UserService? userService;
    public User? CurrentPlayer { get; private set; }

    public string PlayerInitials
    {
        get
        {
            return CurrentPlayer is null ? string.Empty : CurrentPlayer.Username;
        }
    }

    public ProfileViewModel(UserService userService)
    {
        Title = "Profil";
        this.userService = userService;

        this.CurrentPlayer = this.userService.GetUserById(1) ?? new User();

        OnPropertyChanged(nameof(CurrentPlayer));
        OnPropertyChanged(nameof(PlayerInitials));
    }
}