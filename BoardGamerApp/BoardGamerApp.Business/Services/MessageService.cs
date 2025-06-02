using BoardGamerApp.Business.Models;
using BoardGamerApp.Business.Repositories;

namespace BoardGamerApp.Business.Services;
public class MessageService
{
    private readonly IMessageRepository messageRepository;

    public MessageService(IMessageRepository messageRepository)
    {
        this.messageRepository = messageRepository;
    }

    public void AddMessage(string gameSessionId, Message message)
    {
        this.messageRepository.AddMessage(gameSessionId, message);
    }

    public void DeleteMessage(string gameSessionId, string messageId)
    {
        this.messageRepository.DeleteMessage(gameSessionId, messageId);
    }

    public Message? GetMessageById(string gameSessionId, string messageId)
    {
        return this.messageRepository.GetMessageById(gameSessionId, messageId);
    }

    public IEnumerable<Message> GetMessagesByUserId(string gameSessionId, string userId)
    {
        return this.messageRepository.GetMessagesByUserId(gameSessionId, userId);
    }

    public IEnumerable<Message> GetAllMessages(string gameSessionId)
    {
        return this.messageRepository.GetAllMessages(gameSessionId);
    }
}
