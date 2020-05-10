using System.Threading.Tasks;
using RiotPls.DataDragon.Entities;
using Xunit;
using Xunit.Abstractions;

namespace RiotPls.Tests.DataDragon
{
    public class RunesTests : DataDragonTestBase
    {
        [Fact(DisplayName = "Runes API works and deserialization is accurate")]
        public async Task Test_That_RunesApi_Works()
        {
            var runes = await _client.GetRunesAsync(GameVersion.Parse("10.9.1"));
            Assert.Equal(5, runes.Count);
            Assert.Contains(runes, rune => rune.Key == "Domination" && rune.Slots.Count == 4);
            Assert.Contains(runes, rune => rune.Key == "Inspiration" && rune.Slots.Count == 4);
            Assert.Contains(runes, rune => rune.Key == "Precision" && rune.Slots.Count == 4);
            Assert.Contains(runes, rune => rune.Key == "Resolve" && rune.Slots.Count == 4);
            Assert.Contains(runes, rune => rune.Key == "Sorcery" && rune.Slots.Count == 4);
        }
        
        public RunesTests(ITestOutputHelper helper) : base(helper)
        {
        }
    }
}