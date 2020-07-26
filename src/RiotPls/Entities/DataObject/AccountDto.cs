using System.Text.Json.Serialization;

#nullable disable
namespace RiotPls.Entities.DataObject
{
    internal sealed class AccountDto
    {
        [JsonPropertyName("puuid")]
        public string Puuid { get; set; }
        
        [JsonPropertyName("gameName")]
        public string GameName { get; set; }
        
        [JsonPropertyName("tagLine")]
        public string TagLine { get; set; }
    }
}