using System.IO;
using System.Threading.Tasks;

namespace RiotPls.DataDragon.Entities
{
    public sealed class ProfileIcon
    {
        /// <inheritdoc cref="BaseData.Version"/>
        public GameVersion Version { get; }

        /// <summary>
        ///     Id of the profile icon.
        /// </summary>
        public int Id { get; }

        /// <summary>
        ///     Image information of the profile icon.
        /// </summary>
        public ImageInformation Image { get; }

        internal ProfileIcon(ProfileIconDto dto, GameVersion version)
        {
            Id = dto.Id;
            Image = new ImageInformation(dto.Image);
            Version = version;
        }

        /// <summary>
        ///     Downloads this profily icon.
        /// </summary>
        public Task<Stream> DownloadAsync()
            => DataDragonClient.CommunityDragonHttpClient.GetStreamAsync(
                $"{Version}/profile-icon/{Id}");
    }
}