namespace RiotPls.DataDragon.Entities
{
    public class SpellBase
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
        public ImageInformation Image { get; }

        internal SpellBase(SpellBaseDto dto)
        {
            Name = dto.Name;
            RawDescription = dto.RawDescription;
            Image = new ImageInformation(dto.Image);
        }
    }
}