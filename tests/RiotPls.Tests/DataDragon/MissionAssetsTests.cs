using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace RiotPls.Tests.DataDragon
{
    public class MissionAssetsTests : DataDragonTestBase
    {
        public MissionAssetsTests(ITestOutputHelper helper) : base(helper)
        {
        }

        [Fact(DisplayName = "Mission assets API works and deserialization is accurate")]
        public async Task Test_That_MissionAssetsApi_Works()
        {
            var version = await _client.FetchLatestVersionAsync();
            var missionAssets = await _client.GetMissionAssetsAsync(version);
            Assert.NotEmpty(missionAssets);
        }
    }
}