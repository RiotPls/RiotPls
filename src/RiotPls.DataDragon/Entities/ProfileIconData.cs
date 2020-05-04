using System.Collections.ObjectModel;
using System.Linq;

namespace RiotPls.DataDragon.Entities
{
    public sealed class ProfileIconData : BaseData
    {
        /// <summary>
        ///     A dictionary of profile icons, keyed by their unique identifier.
        /// </summary>
        public ReadOnlyDictionary<int, ProfileIcon> ProfileIcons { get; }
        
        internal ProfileIconData(DataDragonClient client, ProfileIconDataDto dto) : base(client, dto)
        {
            ProfileIcons = new ReadOnlyDictionary<int, ProfileIcon>(
                dto.ProfileIcons.ToDictionary(
                    x => int.Parse(x.Key),
                    y => new ProfileIcon(client, y.Value)));
        }
    }
}