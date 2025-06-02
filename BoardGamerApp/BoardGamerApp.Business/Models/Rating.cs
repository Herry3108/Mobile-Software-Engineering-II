using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardGamerApp.Business.Models;
public class Rating
{
    [Key]
    [JsonProperty("ratingId")]
    public int RatingId { get; set; }

    [JsonProperty("gameSessionId")]
    public int GameSessionId { get; set; }

    [ForeignKey("GameSessionId")]
    [JsonIgnore]
    public GameSession GameSession { get; set; }

    [JsonProperty("ratedByUserId")]
    public int RatedByUserId { get; set; }

    [ForeignKey("RatedByUserId")]
    [JsonIgnore]
    public User RatedByUser { get; set; }

    [JsonProperty("hostRating")]
    public string HostRating { get; set; } = string.Empty;

    [JsonProperty("foodRating")]
    public string FoodRating { get; set; } = string.Empty;

    [JsonProperty("overallRating")]
    public string OverallRating { get; set; } = string.Empty;

    [StringLength(500)]
    [JsonProperty("comment")]
    public string Comment { get; set; } = string.Empty;

    [JsonProperty("ratedAt")]
    public DateTime RatedAt { get; set; } = DateTime.Now;

    public Rating() { }
}