using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace RiotPls.DataDragon.Entities
{
    /// <summary>
    ///     Represents information about League of Legends champions.
    /// </summary>
    public class ChampionData
    {
        /// <summary>
        ///     The type of retrieved data.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }
        
        /// <summary>
        ///     The format of this data.
        /// </summary>
        [JsonPropertyName("format")]
        public string Format { get; set; }
        
        /// <summary>
        ///     The version of the data.
        /// </summary>
        [JsonPropertyName("version")]
        public string Version { get; set; }

        /// <summary>
        ///     A dictionary of champion objects, keyed by their unique identifiers.
        /// </summary>
        public IReadOnlyDictionary<string, ChampionBase> Champions =>
            _champions ??= ChampionsRaw.ToDictionary(x => x.Value.Key, y => y.Value);
        
        [JsonPropertyName("data")]
        public IReadOnlyDictionary<string, ChampionBase> ChampionsRaw { get; set; }
        private IReadOnlyDictionary<string, ChampionBase> _champions;
    }
}