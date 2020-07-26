using System.Text.Json.Serialization;

namespace RiotPls.Entities.DataObject
{
    internal sealed class MiniSeriesDto
    {
        [JsonPropertyName("losses")]
        public int Losses { get; set; }
        
        [JsonPropertyName("progress")]
        public string Progress { get; set; }
        
        [JsonPropertyName("target")]
        public int Target { get; set; }
        
        [JsonPropertyName("wins")]
        public int Wins { get; set; }
    }
}