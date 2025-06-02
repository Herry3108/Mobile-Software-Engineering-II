using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BoardGamerApp.Business.Models;
public class User
{
    [Key]
    [JsonProperty("userId")]
    public int UserId { get; set; }

    [Required]
    [StringLength(50)]
    [JsonProperty("username")]
    public string Username { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    [JsonProperty("email")]
    public string Email { get; set; } = string.Empty;

    [StringLength(15)]
    [JsonProperty("phoneNumber")]
    public string PhoneNumber { get; set; } = string.Empty;

    [StringLength(200)]
    [JsonProperty("address")]
    public string Address { get; set; } = string.Empty;

    [JsonProperty("hostedGames")]
    public int HostedGames { get; set; }

    [JsonIgnore]
    public ICollection<GameSession> HostedSessions { get; set; } = new List<GameSession>();

    [JsonIgnore]
    public ICollection<GameProposal> GameProposals { get; set; } = new List<GameProposal>();

    [JsonIgnore]
    public ICollection<Vote> Votes { get; set; } = new List<Vote>();

    [JsonIgnore]
    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    [JsonIgnore]
    public ICollection<Message> SentMessages { get; set; } = new List<Message>();

    [JsonIgnore]
    public ICollection<UserGameSession> AttendedSessions { get; set; } = new List<UserGameSession>();

    public User() { }
}