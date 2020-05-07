namespace RiotPls.DataDragon.Entities
{
    public sealed class ProfileIcon
    {
        /// <summary>
        ///     Id of the profile icon.
        /// </summary>
        public int Id { get; }
        
        /// <summary>
        ///     Image information of the profile icon.
        /// </summary>
        public StaticImage Image { get; }
        
        internal ProfileIcon(ProfileIconDto dto)
        {
            Id = dto.Id;
            Image = new StaticImage(dto.Image);
        }
    }
}