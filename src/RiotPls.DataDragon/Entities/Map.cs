namespace RiotPls.DataDragon.Entities
{
    public sealed class Map
    {
        /// <summary>
        ///     Name of the map.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Id of the map.
        /// </summary>
        public int Id { get; }

        /// <summary>
        ///     Image information of the map.
        /// </summary>
        public ImageInformation Image { get; }

        internal Map(MapDto dto)
        {
            Name = dto.Name;
            Id = int.Parse(dto.Id);
            Image = new ImageInformation(dto.Image);
        }
    }
}