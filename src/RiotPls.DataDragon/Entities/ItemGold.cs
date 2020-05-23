namespace RiotPls.DataDragon.Entities
{
    public class ItemGold
    {
        public long Base { get; }

        public long Total { get; }

        public long Sell { get; }
 
        public bool Purchasable { get; }

        internal ItemGold(ItemGoldDto dto)
        {
            Base = dto.Base;
            Total = dto.Total;
            Sell = dto.Sell;
            Purchasable = dto.Purchasable;
        }
    }
}