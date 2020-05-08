using RiotPls.DataDragon.Entities;
using RiotPls.DataDragon.Enums;

namespace RiotPls.DataDragon.Entities
{
    public sealed class RealmVersion
    {
        public RealmDataVersion DataVersion { get; set; }

        public GameVersion Version { get; set; }

        public Language Language { get; set; }

        public string Cdn { get; set; }

        public GameVersion DataDragonVersion { get; set; }

        public GameVersion Lg { get; set; }

        public GameVersion Css { get; set; }

        public long MaximumProfileIcons { get; set; }

        internal RealmVersion(RealmVersionDto dto)
        {
            DataVersion = new RealmDataVersion(dto.DataVersion);
            Version = dto.Version;
            Language = dto.Language;
            Cdn = dto.Cdn;
            DataDragonVersion = dto.DataDragonVersion;
            Lg = dto.Lg;
            Css = dto.Css;
            MaximumProfileIcons = dto.MaximumProfileIcons;
        }
    }
}
