using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace RiotPls.Tests.DataDragon
{
    public class SpellsTests : DataDragonTestBase
    {
        [Fact(DisplayName = "Summoner spell API works and deserialization is accurate")]
        public async Task Test_That_SummonerSpellApi_Works()
        {
            var versions = await _client.GetVersionsAsync();
            var sums = await _client.GetSummonerSpellsAsync(versions.First());
            Assert.NotNull(sums.SummonerSpells);
            Assert.Equal(14, sums.SummonerSpells.Count);
            Assert.True(sums.SummonerSpells.ContainsKey(21)
                        && sums.SummonerSpells[21].Id == "SummonerBarrier");
        }

        public SpellsTests(ITestOutputHelper helper) : base(helper)
        {
        }
    }
}