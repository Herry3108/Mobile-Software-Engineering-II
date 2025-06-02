using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardGamerApp.Business.Models;
public class UserGameSession
{
    [Key]
    [JsonProperty("userGameSessionId")]
    public int UserGameSessionId { get; set; }

    [JsonProperty("userId")]
    public int UserId { get; set; }

    [ForeignKey("UserId")]
    [JsonIgnore]
    public User User { get; set; }

    [JsonProperty("gameSessionId")]
    public int GameSessionId { get; set; }

    [ForeignKey("GameSessionId")]
    [JsonIgnore]
    public GameSession GameSession { get; set; }

    [JsonProperty("isAttending")]
    public bool IsAttending { get; set; } = false;
}