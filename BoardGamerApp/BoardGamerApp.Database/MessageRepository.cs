using BoardGamerApp.Business.Models;
using BoardGamerApp.Business.Repositories;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;

namespace BoardGamerApp.Database;
public class MessageRepository : IMessageRepository
{
    private readonly IFirebaseClient _client;

    public MessageRepository()
    {
        var config = new FirebaseConfig
        {
            AuthSecret = "ToDo",
            BasePath = "ToDo"
        };
        _client = new FirebaseClient(config);
    }

    public void AddMessage(string gameSessionId, Message message)
    {
        var sessionResponse = _client.Get($"gameSessions/{gameSessionId}");
        if (sessionResponse == null || string.IsNullOrEmpty(sessionResponse.Body) || sessionResponse.Body == "null")
        {
            return;
        }

        _client.Set($"messages/{message.MessageId}", message);
        _client.Set($"gameSession-messages/{gameSessionId}/{message.MessageId}", true);
        _client.Set($"user-messages/{message.SenderId}/{message.MessageId}", true);
    }

    public void DeleteMessage(string gameSessionId, string messageId)
    {
        var sessionResponse = _client.Get($"gameSessions/{gameSessionId}");
        if (sessionResponse == null || string.IsNullOrEmpty(sessionResponse.Body) || sessionResponse.Body == "null")
        {
            return;
        }

        var messageResponse = _client.Get($"messages/{messageId}");
        if (messageResponse == null || string.IsNullOrEmpty(messageResponse.Body) || messageResponse.Body == "null")
        {
            return;
        }

        var message = FirebaseParser.ParseSingle<Message>(messageResponse.Body);
        if (message == null)
        {
            return;
        }

        _client.Delete($"messages/{messageId}");
        _client.Delete($"gameSession-messages/{gameSessionId}/{messageId}");
        _client.Delete($"user-messages/{message.SenderId}/{messageId}");
    }

    public IEnumerable<Message> GetAllMessages(string gameSessionId)
    {
        var messagesResponse = _client.Get($"gameSession-messages/{gameSessionId}");
        if (messagesResponse == null || string.IsNullOrEmpty(messagesResponse.Body) || messagesResponse.Body == "null")
        {
            return Enumerable.Empty<Message>();
        }

        var messageKeys = FirebaseParser.ParseValueDictionary<bool>(messagesResponse.Body);
        var messages = new List<Message>();

        foreach (var key in messageKeys.Keys)
        {
            var messageResponse = _client.Get($"messages/{key}");
            if (messageResponse != null && !string.IsNullOrEmpty(messageResponse.Body) && messageResponse.Body != "null")
            {
                var message = FirebaseParser.ParseSingle<Message>(messageResponse.Body);
                if (message != null)
                {
                    messages.Add(message);
                }
            }
        }

        return messages;
    }

    public Message? GetMessageById(string gameSessionId, string messageId)
    {
        var sessionMessagesResponse = _client.Get($"gameSession-messages/{gameSessionId}/{messageId}");
        if (sessionMessagesResponse == null || string.IsNullOrEmpty(sessionMessagesResponse.Body) || sessionMessagesResponse.Body == "null")
        {
            return null;
        }

        var messageResponse = _client.Get($"messages/{messageId}");
        if (messageResponse == null || string.IsNullOrEmpty(messageResponse.Body) || messageResponse.Body == "null")
        {
            return null;
        }

        return FirebaseParser.ParseSingle<Message>(messageResponse.Body);
    }

    public IEnumerable<Message> GetMessagesByUserId(string gameSessionId, string userId)
    {
        var allMessages = GetAllMessages(gameSessionId);
        return allMessages.Where(m => m.SenderId.ToString().Equals(userId));
    }
}