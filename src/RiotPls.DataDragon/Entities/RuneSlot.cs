namespace RiotPls.DataDragon.Entities
{
    public class RuneSlot
    {
        /// <summary>
        ///     Id of the rune slot.
        /// </summary>
        public int Id { get; }

        /// <summary>
        ///     Key of the rune slot.
        /// </summary>
        public string Key { get; }
        
        /// <summary>
        ///     Icon path of the rune slot.
        /// </summary>
        public string Icon { get; }
        
        /// <summary>
        ///     Name of the rune slot.
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        ///     Short description of the rune slot effect.
        /// </summary>
        public string ShortDescription { get; }
        
        /// <summary>
        ///     Long description of the rune slot effect.
        /// </summary>
        public string LongDescription { get; }

        internal RuneSlot(RuneSlotDto dto)
        {
            Id = dto.Id;
            Key = dto.Key;
            Icon = dto.Icon;
            Name = dto.Name;
            ShortDescription = dto.ShortDescription;
            LongDescription = dto.LongDescription;
        }
    }
}