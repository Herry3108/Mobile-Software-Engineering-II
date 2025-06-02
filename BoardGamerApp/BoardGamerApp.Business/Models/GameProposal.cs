using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardGamerApp.Business.Models;
public class GameProposal
{
    [Key]
    [JsonProperty("gameProposalId")]
    public int GameProposalId { get; set; }

    [JsonProperty("gameId")]
    public int GameId { get; set; }

    [ForeignKey("GameId")]
    [JsonIgnore]
    public Game Game { get; set; }

    [JsonProperty("gameSessionId")]
    public int GameSessionId { get; set; }

    [ForeignKey("GameSessionId")]
    [JsonIgnore]
    public GameSession GameSession { get; set; }

    [JsonProperty("proposedByUserId")]
    public int ProposedByUserId { get; set; }

    [JsonProperty("proposedAt")]
    public DateTime ProposedAt { get; set; } = DateTime.Now;

    [JsonIgnore]
    public ICollection<Vote> Votes { get; set; } = new List<Vote>();

    public GameProposal() { }
}