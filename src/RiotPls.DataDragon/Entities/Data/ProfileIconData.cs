using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RiotPls.DataDragon.Entities
{
    internal sealed class ProfileIconData : BaseData
    {
        /// <summary>
        ///     A dictionary of profile icons, keyed by their unique identifier.
        /// </summary>
        public ReadOnlyDictionary<int, ProfileIcon> ProfileIcons { get; }

        internal ProfileIconData(ProfileIconDataDto dto) : base(dto)
        {
            ProfileIcons = new ReadOnlyDictionary<int, ProfileIcon>(
                dto.ProfileIcons.ToDictionary(
                    x => int.Parse(x.Key),
                    Converter));
        }

        private ProfileIcon Converter(KeyValuePair<string, ProfileIconDto> kvp)
            => new ProfileIcon(kvp.Value, Version);
    }
}