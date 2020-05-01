using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json.Serialization;
using RiotPls.DataDragon.Converters;

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
        public string Type { get; }
        
        /// <summary>
        ///     The format of this data.
        /// </summary>
        public string Format { get; }
        
        /// <summary>
        ///     The version of the data.
        /// </summary>
        public GameVersion Version { get; }

        /// <summary>
        ///     A dictionary of champion objects, keyed by their unique identifiers.
        /// </summary>
        public IReadOnlyDictionary<int, ChampionBase> Champions { get; }
        
        internal ChampionData(ChampionDataDto dto)
        {
            Type = dto.Type;
            Format = dto.Format;
            Version = dto.Version;
            Champions = new ReadOnlyDictionary<int, ChampionBase>(
                dto.Champions.ToDictionary(
                x => x.Value.Key, y => new ChampionBase(y.Value)));
        }
    }
}