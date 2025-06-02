using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BoardGamerApp.Business.Models;
public class Game
{
    [Key]
    [JsonProperty("gameId")]
    public int GameId { get; set; }

    [StringLength(100)]
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("description")]
    public string Description { get; set; } = string.Empty;

    public Game() { }
}