using System;
using System.Collections.Generic;
using System.Linq;
using RiotPls.DataDragon.Enums;

namespace RiotPls.DataDragon.Entities
{
    public class ChampionBase
    {
        /// <summary>
        ///     The version of the data.
        /// </summary>
        public GameVersion Version { get; }

        /// <summary>
        ///     The unique identifier of the champion.
        /// </summary>
        public string Id { get; }

        /// <summary>
        ///     The unique key of the champion.
        /// </summary>
        public int Key { get; }

        /// <summary>
        ///     The name of the champion.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     The title of the champion.
        /// </summary>
        public string Title { get; }

        /// <summary>
        ///     The summary of the champion.
        /// </summary>
        public string Blurb { get; }

        /// <summary>
        ///     The resource type of the champion. (mana, energy, fury)
        /// </summary>
        public ResourceType ResourceType { get; }

        /// <summary>
        ///     Statistics about the champion.
        /// </summary>
        public ChampionInfo Info { get; }

        /// <summary>
        ///     Information and name of the images associated with that champion.
        /// </summary>
        public StaticImage Image { get; }

        /// <summary>
        ///     Tags representing the champion.
        /// </summary>
        public IReadOnlyCollection<ChampionType> Tags { get; }

        /// <summary>
        ///     Statistics related to the champion.
        /// </summary>
        public ChampionStats Stats { get; }

        internal ChampionBase(ChampionBaseDto dto)
        {
            Version = dto.Version;
            Id = dto.Id;
            Key = int.Parse(dto.Key);
            Name = dto.Name;
            Title = dto.Title;
            Blurb = dto.Blurb;
            ResourceType = Enum.Parse<ResourceType>(dto.Partype.Replace(" ", ""), true);
            Info = new ChampionInfo(dto.Info);
            Image = new StaticImage(dto.Image);
            Tags = dto.Tags.Select(x => Enum.Parse<ChampionType>(x, true)).ToList().AsReadOnly();
            Stats = new ChampionStats(dto.Stats);
        }
    }
}