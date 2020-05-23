namespace RiotPls.DataDragon.Entities
{
    public class ItemGold
    {
        /// <summary>
        ///     Base amount of gold.
        /// </summary>
        public long Base { get; }

        /// <summary>
        ///     Total amount of gold.
        /// </summary>
        public long Total { get; }

        /// <summary>
        ///     Amount of gold given when sold.
        /// </summary>
        public long Sell { get; }
 
        /// <summary>
        ///     Indicates whether the item is purchasable. 
        /// </summary>
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