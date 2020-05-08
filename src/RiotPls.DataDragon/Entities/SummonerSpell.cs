namespace RiotPls.DataDragon.Entities
{
    public sealed class SummonerSpell : Spell
    {
        /// <summary>
        ///     Key of this summoner spell.
        /// </summary>
        public int Key { get; }

        /// <summary>
        ///     Summoner level required to use this summoner spell.
        /// </summary>
        public int SummonerLevel { get; }

        internal SummonerSpell(SummonerSpellDto dto) : base(dto)
        {
            Key = int.Parse(dto.Key);
            SummonerLevel = dto.SummonerLevel;
        }
    }
}