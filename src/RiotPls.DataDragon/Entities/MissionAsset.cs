namespace RiotPls.DataDragon.Entities
{
    public class MissionAsset
    {
        /// <summary>
        ///     Id of the mission.
        /// </summary>
        public int Id { get; }

        /// <summary>
        ///     Image of the mission asset.
        /// </summary>
        public StaticImage Image { get; }

        internal MissionAsset(MissionAssetDto dto)
        {
            Id = dto.Id;
            Image = new StaticImage(dto.Image);
        }
    }
}