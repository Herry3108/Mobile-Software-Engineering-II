using BoardGamerApp.Business.Models;
using BoardGamerApp.Business.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BoardGamerApp.ViewModels;

public class MessagesViewModel : BaseViewModel
{
    public ObservableCollection<Message> Messages { get; private set; }

    private string _newMessage = string.Empty;
    public string NewMessage
    {
        get => _newMessage;
        set => SetProperty(ref _newMessage, value);
    }

    public ICommand SendMessageCommand { get; }
    private readonly MessageService messageService;

    public MessagesViewModel(MessageService messageService)
    {
        Title = "Nachrichten";
        this.messageService = messageService;

        var messages = this.messageService.GetAllMessages(string.Empty);
        this.Messages = messages != null
            ? new ObservableCollection<Message>(messages)
            : new ObservableCollection<Message>();

        SendMessageCommand = new Command(SendMessage);
    }

    private void SendMessage()
    {
        if (string.IsNullOrWhiteSpace(NewMessage))
        {
            return;
        }

        var message = new Message()
        {
            Content = this.NewMessage,
            SenderId = 1,
            SentAt = DateTime.Now
        };

        this.messageService.AddMessage(string.Empty, message);
        this.Messages.Add(message);
        this.NewMessage = string.Empty;

        OnPropertyChanged(nameof(Messages));
    }
}