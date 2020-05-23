namespace RiotPls.DataDragon.Entities
{
    public class ItemEffect
    {
        public string Effect1Amount { get; }

        public string Effect2Amount { get; }

        public string Effect3Amount { get; }

        public string Effect4Amount { get; }

        public string Effect5Amount { get; }

        public string Effect6Amount { get; }

        public string Effect7Amount { get; }

        public string Effect8Amount { get; }

        public string Effect9Amount { get; }

        public string Effect10Amount { get; }

        public string Effect11Amount { get; }

        public string Effect12Amount { get; }

        public string Effect13Amount { get; }

        public string Effect14Amount { get; }

        public string Effect15Amount { get; }

        public string Effect16Amount { get; }

        public string Effect17Amount { get; }

        public string Effect18Amount { get; }
        
        internal ItemEffect(ItemEffectDto dto)
        {
            Effect1Amount = dto.Effect1Amount;
            Effect2Amount = dto.Effect2Amount;
            Effect3Amount = dto.Effect3Amount;
            Effect4Amount = dto.Effect4Amount;
            Effect5Amount = dto.Effect5Amount;
            Effect6Amount = dto.Effect6Amount;
            Effect7Amount = dto.Effect7Amount;
            Effect8Amount = dto.Effect8Amount;
            Effect9Amount = dto.Effect9Amount;
            Effect10Amount = dto.Effect10Amount;
            Effect11Amount = dto.Effect11Amount;
            Effect12Amount = dto.Effect12Amount;
            Effect13Amount = dto.Effect13Amount;
            Effect14Amount = dto.Effect14Amount;
            Effect15Amount = dto.Effect15Amount;
            Effect16Amount = dto.Effect16Amount;
            Effect17Amount = dto.Effect17Amount;
            Effect18Amount = dto.Effect18Amount;
        }
    }
}