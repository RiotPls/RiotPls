﻿using System.Collections.ObjectModel;
using System.Linq;

namespace RiotPls.DataDragon.Entities
{
    internal class MapData : BaseData
    {
        /// <summary>
        ///     A dictionary of map objects, keyed by their unique identifiers.
        /// </summary>
        public ReadOnlyDictionary<int, Map> Maps { get; }

        internal MapData(MapDataDto dto) : base(dto)
        {
            Maps = new ReadOnlyDictionary<int, Map>(
                dto.Maps.ToDictionary(
                    x => int.Parse(x.Key),
                    y => new Map(y.Value)));
        }
    }
}