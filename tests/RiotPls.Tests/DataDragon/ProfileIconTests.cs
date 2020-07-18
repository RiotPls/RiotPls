using System;
using System.Linq;
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
            var version = await _client.FetchLatestVersionAsync();
            var profileIcons = await _client.GetProfileIconsAsync(version);
            Assert.NotNull(profileIcons);
            Assert.Equal(0, profileIcons.First().Id);
        }

        public ProfileIconTests(ITestOutputHelper helper) : base(helper)
        {
        }
    }
}