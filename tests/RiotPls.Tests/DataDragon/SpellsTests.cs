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
            Assert.NotNull(sums);
            Assert.Equal(14, sums.Count);
            Assert.True(sums.Any(x => x.Key == 21) && sums.First(x => x.Key == 21).Id == "SummonerBarrier");
        }

        public SpellsTests(ITestOutputHelper helper) : base(helper)
        {
        }
    }
}