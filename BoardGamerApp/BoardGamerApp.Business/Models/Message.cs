using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardGamerApp.Business.Models;
public class Message
{
    [Key]
    [JsonProperty("messageId")]
    public int MessageId { get; set; }

    [JsonProperty("senderId")]
    public int SenderId { get; set; }

    [ForeignKey("SenderId")]
    [JsonIgnore]
    public User Sender { get; set; }

    [JsonProperty("gameSessionId")]
    public int GameSessionId { get; set; }

    [ForeignKey("GameSessionId")]
    [JsonIgnore]
    public GameSession GameSession { get; set; }

    [JsonProperty("content")]
    public string Content { get; set; } = string.Empty;

    [JsonProperty("sentAt")]
    public DateTime SentAt { get; set; } = DateTime.Now;

    public Message() { }
}