using System.Collections.ObjectModel;
using System.Linq;

namespace RiotPls.DataDragon.Entities
{
    public class MissionAssetData : BaseData
    {
        /// <summary>
        ///     A dictionary of mission asset objects, keyed by their unique mission identifiers.
        /// </summary>
        public ReadOnlyDictionary<string, MissionAsset> MissionAssets { get; }

        internal MissionAssetData(MissionAssetDataDto dto) : base(dto)
        {
            MissionAssets = new ReadOnlyDictionary<string, MissionAsset>(
                dto.MissionAssets.ToDictionary(
                    x => x.Key,
                    y => new MissionAsset(y.Value)));
        }
    }
}