namespace RiotPls.DataDragon.Entities
{
    public sealed class ProfileIcon
    {
        private readonly DataDragonClient _client;

        /// <summary>
        ///     Id of the profile icon.
        /// </summary>
        public int Id { get; }
        
        /// <summary>
        ///     Image information of the profile icon.
        /// </summary>
        public StaticImage Image { get; }
        
        internal ProfileIcon(DataDragonClient client, ProfileIconDto dto)
        {
            _client = client;
            
            Id = dto.Id;
            Image = new StaticImage(dto.Image);
        }
    }
}