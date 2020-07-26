using System.Text.Json.Serialization;

#nullable disable
namespace RiotPls.Entities.DataObject
{
    internal sealed class LeagueItemDto
    {
        [JsonPropertyName("freshBlood")]
        public bool FreshBlood { get; set; }
        
        [JsonPropertyName("wins")]
        public int Wins { get; set; }
        
        [JsonPropertyName("summonerName")]
        public string SummonerName { get; set; }
        
        [JsonPropertyName("miniSeries")]
        public MiniSeriesDto MiniSeries { get; set; }
        
        [JsonPropertyName("inactive")]
        public bool Inactive { get; set; }
        
        [JsonPropertyName("veteran")]
        public bool Veteran { get; set; }
        
        [JsonPropertyName("hotStreak")]
        public bool HotStreak { get; set; }
        
        [JsonPropertyName("rank")]
        public string Rank { get; set; }
        
        [JsonPropertyName("leaguePoints")]
        public int LeaguePoints { get; set; }
        
        [JsonPropertyName("losses")]
        public int Losses { get; set; }
        
        [JsonPropertyName("summonerId")]
        public string SummonerId { get; set; }
    }
}