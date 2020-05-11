namespace RiotPls.DataDragon.Entities
{
    internal class BaseData
    {
        public string Type { get; }

        public string Format { get; }

        public GameVersion Version { get; }

        internal BaseData(BaseDataDto dto)
        {
            Type = dto.Type;
            Format = dto.Format;
            Version = dto.Version;
        }
    }
}