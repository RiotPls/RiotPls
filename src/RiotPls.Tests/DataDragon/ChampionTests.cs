using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RiotPls.Tests.DataDragon
{
    public class ChampionTests : DataDragonTestBase
    {
        [Fact(DisplayName = "Version is properly parsed into a GameVersion object.")]
        public async Task Test_That_Version_IsProperlyParsedInGameVersion()
        {
            var versions = await _client.GetVersionsAsync();
            var latestVersion = versions.First();

            var champions = await _client.GetChampionsAsync(latestVersion);
            Assert.NotNull(champions.Version);
            Assert.Equal(champions.Version.Major, latestVersion.Major);
            Assert.Equal(champions.Version.Minor, latestVersion.Minor);
            Assert.Equal(champions.Version.Patch, latestVersion.Patch);
        }
    }
}