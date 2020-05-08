using System.Text.Json.Serialization;
using RiotPls.DataDragon.Enums;
#nullable disable
namespace RiotPls.DataDragon.Entities
{
    internal class RealmVersionDto
    {
        [JsonPropertyName("n")]
        public RealmDataVersionsDto DataVersion { get; set; }

        [JsonPropertyName("v")]
        public GameVersion Version { get; set; }

        [JsonPropertyName("l")]
        public Language Language { get; set; }

        [JsonPropertyName("cdn")]
        public string Cdn { get; set; }

        [JsonPropertyName("dd")]
        public GameVersion DataDragonVersion { get; set; }

        [JsonPropertyName("lg")]
        public GameVersion Lg { get; set; }

        [JsonPropertyName("css")]
        public GameVersion Css { get; set; }

        [JsonPropertyName("profileiconmax")]
        public long MaximumProfileIcons { get; set; }

        //[JsonPropertyName("store")]
        [JsonIgnore] // Always null it seems
        public object Store { get; set; }
    }
}
