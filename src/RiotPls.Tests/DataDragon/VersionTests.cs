using System;
using System.Linq;
using System.Threading.Tasks;
using RiotPls.DataDragon;
using RiotPls.DataDragon.Entities;
using Xunit;

namespace RiotPls.Tests.DataDragon
{
    public class VersionTests : DataDragonTestBase
    {
        [Fact(DisplayName = "Version API works and deserialization is accurate")]
        public async Task Test_That_VersionApi_Works()
        {
            var versions = await _client.GetVersionsAsync();
            Assert.True(versions.Count > 10, $"Expected there to be at least 10 versions in response, got {versions.Count}");
        }

        [Theory(DisplayName = "Parsing versions and writing them to strings works and is accurate")]
        [InlineData("10.9.1", 10, 9, 1)]
        [InlineData("0.154.3", 0, 154, 3)]
        [InlineData("lolpatch_4.4", 4, 4)]
        [InlineData("lolpatch_3.7", 3, 7)]
        public void Test_That_Parsing_Versions_Works(string versionString, int versionMajor, int versionMinor, int? versionPatch = null)
        {
            var versionParsed = GameVersion.Parse(versionString);
            Assert.Equal(versionMajor, versionParsed.Major);
            Assert.Equal(versionMinor, versionParsed.Minor);
            if (versionPatch != null) Assert.Equal(versionPatch, versionParsed.Patch);
        }
    }
}