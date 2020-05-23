using System.Collections.Generic;
using System.Text.Json.Serialization;
using RiotPls.DataDragon.Converters;
#nullable disable
namespace RiotPls.DataDragon.Entities
{
    internal class ItemDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("colloq")]
        public string Colloq { get; set; }

        [JsonPropertyName("plaintext")]
        public string Plaintext { get; set; }

        [JsonPropertyName("into")]
        public string[] Into { get; set; }

        [JsonPropertyName("image")]
        public StaticImageDto Image { get; set; }

        [JsonPropertyName("gold")]
        public ItemGoldDto Gold { get; set; }

        [JsonPropertyName("tags")]
        public string[] Tags { get; set; }

        [JsonPropertyName("maps")]
        public IReadOnlyDictionary<string, bool> Maps { get; set; }

        [JsonPropertyName("stats")]
        public IReadOnlyDictionary<string, double> Stats { get; set; }

        [JsonPropertyName("inStore")]
        public bool? InStore { get; set; }

        [JsonPropertyName("from")]
        public string[] From { get; set; }

        [JsonPropertyName("effect")]
        public ItemEffectDto Effect { get; set; }

        [JsonPropertyName("depth")]
        public long? Depth { get; set; }

        [JsonPropertyName("stacks")]
        public long? Stacks { get; set; }

        [JsonPropertyName("consumed")]
        public bool? Consumed { get; set; }

        [JsonPropertyName("hideFromAll")]
        public bool? HideFromAll { get; set; }

        [JsonPropertyName("consumeOnFull")]
        public bool? ConsumeOnFull { get; set; }

        [JsonPropertyName("specialRecipe")]
        public long? SpecialRecipe { get; set; }

        [JsonPropertyName("requiredChampion")]
        public string RequiredChampion { get; set; }

        [JsonPropertyName("requiredAlly")]
        public string RequiredAlly { get; set; }
    }
}