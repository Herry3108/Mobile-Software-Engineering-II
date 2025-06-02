using BoardGamerApp.Business.Models;

namespace BoardGamerApp.Business.Repositories;
public interface IMessageRepository
{
    void AddMessage(string gameSessionId, Message message);
    void DeleteMessage(string gameSessionId, string messageId);
    Message? GetMessageById(string gameSessionId, string messageId);
    IEnumerable<Message> GetMessagesByUserId(string gameSessionId, string userId);
    IEnumerable<Message> GetAllMessages(string gameSessionId);
}
