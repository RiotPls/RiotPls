using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace RiotPls.Tests.DataDragon
{
    public class ChampionTests : DataDragonTestBase
    {
        [Fact(DisplayName = "Champions can properly be parsed without exceptions.")]
        public async Task Test_That_Champions_AreProperlyParsed()
        {
            var versions = await _client.GetVersionsAsync();
            var latestVersion = versions.First();
            var count = 0;

            await foreach (var champion in _client.GetChampionsAsync(latestVersion))
                count++;

            Assert.True(count > 100);
        }

        [Fact(DisplayName = "Champions can properly be parsed without exceptions.")]
        public async Task Test_That_FullChampions_AreProperlyParsed()
        {
            var versions = await _client.GetVersionsAsync();
            var latestVersion = versions.First();

            await foreach (var champion in _client.GetChampionsAsync(latestVersion))
            {
                Assert.NotNull(champion.Version);
                Assert.NotNull(champion.Recommendations);
                Assert.NotNull(champion.Skins);
                Assert.NotNull(champion.Spells);
                Assert.NotNull(champion.AllyTips);
                Assert.NotNull(champion.EnemyTips);
                _output.WriteLine($"Testing {champion.Name}: pass");
            }
        }

        public ChampionTests(ITestOutputHelper helper) : base(helper)
        {
        }
    }
}