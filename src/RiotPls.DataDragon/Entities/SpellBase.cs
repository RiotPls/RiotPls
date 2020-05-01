namespace RiotPls.DataDragon.Entities
{
    public class SpellBase
    {
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

        internal SpellBase(SpellBaseDto dto)
        {
            Name = dto.Name;
            DescriptionRaw = dto.DescriptionRaw;
            Image = new StaticImage(dto.Image);
        }
    }
}