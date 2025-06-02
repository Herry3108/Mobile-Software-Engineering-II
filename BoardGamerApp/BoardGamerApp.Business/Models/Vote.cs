using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardGamerApp.Business.Models;
public class Vote
{
    [Key]
    [JsonProperty("voteId")]
    public int VoteId { get; set; }

    [JsonProperty("userId")]
    public int UserId { get; set; }

    [ForeignKey("UserId")]
    [JsonIgnore]
    public User User { get; set; }

    [JsonProperty("gameProposalId")]
    public int GameProposalId { get; set; }

    [ForeignKey("GameProposalId")]
    [JsonIgnore]
    public GameProposal GameProposal { get; set; }

    [JsonProperty("isPositive")]
    public bool IsPositive { get; set; } = false;

    [JsonProperty("votedAt")]
    public DateTime VotedAt { get; set; } = DateTime.Now;

    public Vote() { }
}