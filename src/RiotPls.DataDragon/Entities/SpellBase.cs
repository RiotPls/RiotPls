namespace RiotPls.DataDragon.Entities
{
    public sealed class SpellBase
    {
        /// <summary>
        ///     Name of the spell.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Raw description of the spell.
        /// </summary>
        public string RawDescription { get; }

        /// <summary>
        ///     Image of the spell.
        /// </summary>
        public StaticImage Image { get; }

        internal SpellBase(SpellBaseDto dto)
        {
            Name = dto.Name;
            RawDescription = dto.RawDescription;
            Image = new StaticImage(dto.Image);
        }
    }
}