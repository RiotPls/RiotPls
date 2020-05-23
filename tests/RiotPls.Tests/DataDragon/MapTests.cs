using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace RiotPls.Tests.DataDragon
{
    public class MapTests : DataDragonTestBase
    {
        public MapTests(ITestOutputHelper helper) : base(helper)
        {
        }

        [Fact(DisplayName = "Maps API works and deserialization is accurate")]
        public async Task Test_That_MapsApi_Works()
        {
            var version = await _client.FetchLatestVersionAsync();
            var maps = await _client.GetMapsAsync(version);
            Assert.NotEmpty(maps);
            Assert.Equal(3, maps.Count);
        }
    }
}