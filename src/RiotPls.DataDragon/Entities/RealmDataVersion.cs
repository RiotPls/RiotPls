namespace RiotPls.DataDragon.Entities
{
    public sealed class RealmDataVersion
    {
        public GameVersion ItemVersion { get; set; }

        public GameVersion RuneVersion { get; set; }

        public GameVersion MasteryVersion { get; set; }

        public GameVersion SummonerVersion { get; set; }


        public GameVersion ChampionVersion { get; set; }

        public GameVersion ProfileIconVersion { get; set; }

        public GameVersion MapVersion { get; set; }

        public GameVersion LanguageVersion { get; set; }

        public GameVersion StickerVersion { get; set; }

        internal RealmDataVersion(RealmDataVersionsDto dto)
        {
            ItemVersion = dto.ItemVersion;
            RuneVersion = dto.RuneVersion;
            MasteryVersion = dto.MasteryVersion;
            SummonerVersion = dto.SummonerVersion;
            ChampionVersion = dto.ChampionVersion;
            ProfileIconVersion = dto.ProfileIconVersion;
            MapVersion = dto.MapVersion;
            LanguageVersion = dto.LanguageVersion;
            StickerVersion = dto.StickerVersion;
        }
    }
}
