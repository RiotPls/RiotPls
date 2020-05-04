namespace RiotPls.DataDragon.Entities
{
    public sealed class ItemBlock
    {
        private readonly DataDragonClient _client;

        /// <summary>
        ///     Id of the item.
        /// </summary>
        public int Id { get; }
        
        /// <summary>
        ///     Amount of time the item is recommended.
        /// </summary>
        public int Count { get; }

        /// <summary>
        ///     Whether the count is hidden or not.
        /// </summary>
        public bool HideCount { get; }

        internal ItemBlock(DataDragonClient client, ItemBlockDto dto)
        {
            _client = client;
            
            Id = int.Parse(dto.Id);
            Count = dto.Count;
            HideCount = dto.HideCount;
        }
    }
}