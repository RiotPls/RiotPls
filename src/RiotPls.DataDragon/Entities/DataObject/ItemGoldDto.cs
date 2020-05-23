using System.Text.Json.Serialization;
#nullable disable
namespace RiotPls.DataDragon.Entities
{
    internal class ItemGoldDto
    {
        [JsonPropertyName("base")] 
        public long Base { get; set; }

        [JsonPropertyName("total")] 
        public long Total { get; set; }

        [JsonPropertyName("sell")] 
        public long Sell { get; set; }

        [JsonPropertyName("purchasable")] 
        public bool Purchasable { get; set; }
    }
}