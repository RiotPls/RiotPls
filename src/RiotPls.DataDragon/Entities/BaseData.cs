namespace RiotPls.DataDragon.Entities
{
    public class BaseData
    {
        /// <summary>
        ///     The type of retrieved data.
        /// </summary>
        public string Type { get; }

        /// <summary>
        ///     The format of this data.
        /// </summary>
        public string Format { get; }

        /// <summary>
        ///     The version of the data.
        /// </summary>
        public GameVersion Version { get; }

        internal BaseData(BaseDataDto dto)
        {
            Type = dto.Type;
            Format = dto.Format;
            Version = dto.Version;
        }
    }
}