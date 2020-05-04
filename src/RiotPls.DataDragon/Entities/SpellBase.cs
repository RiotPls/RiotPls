namespace RiotPls.DataDragon.Entities
{
    public sealed class SpellBase
    {
        private readonly DataDragonClient _client;

        /// <summary>
        ///     Name of the spell.
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        ///     Description of the spell.
        /// </summary>
        public string DescriptionRaw { get; }

        /// <summary>
        ///     Image of the spell.
        /// </summary>
        public StaticImage Image { get; }

        internal SpellBase(DataDragonClient client, SpellBaseDto dto)
        {
            _client = client;
            Name = dto.Name;
            DescriptionRaw = dto.DescriptionRaw;
            Image = new StaticImage(dto.Image);
        }
    }
}