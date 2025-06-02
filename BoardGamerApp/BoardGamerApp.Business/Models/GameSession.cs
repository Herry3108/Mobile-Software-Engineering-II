using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardGamerApp.Business.Models;
public class GameSession
{
    [Key]
    [JsonProperty("gameSessionId")]
    public int GameSessionId { get; set; }

    [JsonProperty("dateTime")]
    public DateTime? DateTime { get; set; }

    [JsonProperty("userId")]
    public int UserId { get; set; }

    [ForeignKey("UserId")]
    [JsonIgnore]
    public User User { get; set; }

    [StringLength(200)]
    [JsonProperty("location")]
    public string Location { get; set; } = string.Empty;

    [JsonProperty("notes")]
    public string Notes { get; set; } = string.Empty;

    [JsonProperty("isFinished")]
    public bool IsFinished { get; set; }

    [JsonProperty("gameId")]
    public int GameId { get; set; }

    [ForeignKey("GameId")]
    [JsonIgnore]
    public Game Game { get; set; }

    [JsonIgnore]
    public ICollection<UserGameSession> Attendees { get; set; } = new List<UserGameSession>();

    [JsonIgnore]
    public ICollection<GameProposal> GameProposals { get; set; } = new List<GameProposal>();

    [JsonIgnore]
    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    [JsonIgnore]
    public ICollection<Message> Messages { get; set; } = new List<Message>();

    public GameSession() { }
}