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

            var championsData = await _client.GetChampionsAsync(latestVersion);
            Assert.True(championsData.Count > 100);
        }

        [Fact(DisplayName = "Champions can properly be parsed without exceptions.")]
        public async Task Test_That_FullChampions_AreProperlyParsed()
        {
            var versions = await _client.GetVersionsAsync();
            var latestVersion = versions.First();

            var championsData = await _client.GetChampionsAsync(latestVersion);
            Assert.NotNull(championsData);
            foreach (var champion in championsData)
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