using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace RiotPls.Tests.DataDragon
{
    public class ProfileIconTests : DataDragonTestBase
    {
        [Fact(DisplayName = "Profile icon API works and deserialization is accurate")]
        public async Task Test_That_SummonerSpellApi_Works()
        {
            var versions = await _client.GetVersionsAsync();
            var profileIcons = await _client.GetProfileIconsAsync(versions[0]);
            Assert.NotNull(profileIcons.ProfileIcons);
            Assert.Equal(0, profileIcons.ProfileIcons[0].Id);
        }

        public ProfileIconTests(ITestOutputHelper helper) : base(helper)
        {
        }
    }
}