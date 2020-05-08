using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace RiotPls.Tests.DataDragon
{
    public class ChampionTests : DataDragonTestBase
    {
        [Fact(DisplayName = "Version is properly parsed into a GameVersion object.")]
        public async Task Test_That_Version_IsProperlyParsedInGameVersion()
        {
            var versions = await _client.GetVersionsAsync();
            var latestVersion = versions.First();

            var champions = await _client.GetPartialChampionsAsync(latestVersion);
            Assert.NotNull(champions.Version);
            Assert.Equal(champions.Version.Major, latestVersion.Major);
            Assert.Equal(champions.Version.Minor, latestVersion.Minor);
            Assert.Equal(champions.Version.Patch, latestVersion.Patch);
        }

        [Fact(DisplayName = "Champions can properly be parsed without exceptions.")]
        public async Task Test_That_Champions_AreProperlyParsed()
        {
            var versions = await _client.GetVersionsAsync();
            var latestVersion = versions.First();

            var championsData = await _client.GetPartialChampionsAsync(latestVersion);
            foreach (var championData in championsData.Champions.Values)
            {
                _output.WriteLine($"Trying to deserialize {championData.Id}...");
                var champion = await _client.GetChampionAsync(championData.Id, latestVersion);
                Assert.NotNull(champion.Champion);
            }
        }

        [Fact(DisplayName = "Champions can properly be parsed without exceptions.")]
        public async Task Test_That_FullChampions_AreProperlyParsed()
        {
            var versions = await _client.GetVersionsAsync();
            var latestVersion = versions.First();

            var championsData = await _client.GetChampionsAsync(latestVersion);
            Assert.NotNull(championsData);
        }

        public ChampionTests(ITestOutputHelper helper) : base(helper)
        {
        }
    }
}