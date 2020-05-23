using System;
using System.Threading.Tasks;
using RiotPls.DataDragon.Entities;
using Xunit;
using Xunit.Abstractions;

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

        [Theory(DisplayName = "Parsing versions should fail if any number is negative")]
        [InlineData("1.-2.3")]
        [InlineData("-1.2.3")]
        [InlineData("1.2.-3")]
        [InlineData("lolpatch_-1.2")]
        [InlineData("lolpatch_1.-2")]
        public void Test_That_Parsing_Negatives_Is_Not_Allowed(string version)
        {
            Assert.False(GameVersion.TryParse(version, out _));
            Assert.Throws<FormatException>(() => GameVersion.Parse(version));
        }

        [Fact(DisplayName = "Versions should compare properly")]
        public void Test_That_Versions_Compare_Properly()
        {
            var lVersion = GameVersion.Parse("1.2.3");
            var rVersion = GameVersion.Parse("3.2.1");

            Assert.True(rVersion > lVersion, $"{rVersion} should be higher than {lVersion}");
            Assert.True(rVersion >= lVersion, $"{rVersion} should be higher than or equal to {lVersion}");
            Assert.Equal(1, rVersion.CompareTo(lVersion));
            Assert.False(rVersion == lVersion, $"{rVersion} should not be equal to {lVersion}");
            Assert.False(rVersion < lVersion, $"{rVersion} should not be lower than {lVersion}");
            Assert.False(rVersion <= lVersion, $"{rVersion} should not be lower than or equal to {lVersion}");
            Assert.Equal(-1, lVersion.CompareTo(rVersion));

            Assert.True(rVersion > null, $"{rVersion} should be higher than null");
            Assert.False(rVersion == null, $"{rVersion} should not be equal to null");
            Assert.False(rVersion < null, $"{rVersion} should not be lower than null");

            Assert.True(null < rVersion, $"null should be lower than {rVersion}");
            Assert.False(null == rVersion, $"null should not be equal to {rVersion}");
            Assert.False(null > rVersion, $"null should not be higher than {rVersion}");

            var version = GameVersion.Parse("1.33.7");
            var same = GameVersion.Parse("1.33.7");

            Assert.True(version == same, $"{version} should be equal to {same}");
            Assert.True(version >= same, $"{version} should be higher than or equal to {same}");
            Assert.True(version <= same, $"{version} should be lower than or equal to {same}");
            Assert.False(version > same, $"{version} should not be higher than {same}");
            Assert.False(version < same, $"{version} should not be lower than {same}");
            Assert.Equal(0, version.CompareTo(same));
            Assert.Equal(0, same.CompareTo(version));

            GameVersion @null = null;

            Assert.True(@null == null, "@null should be equal to null");
            Assert.True(@null >= null, "@null should be higher than or equal to null");
            Assert.True(@null <= null, "@null should be lower than or equal to null");
            Assert.False(@null > null, "@null should not be higher than null");
            Assert.False(@null < null, "@null should not be lower than null");
            Assert.True(null == @null, "null should be equal to @null");
            Assert.True(null >= @null, "null should be higher than or equal to @null");
            Assert.True(null <= @null, "null should be lower than or equal to @null");
            Assert.False(null > @null, "null should not be higher than @null");
            Assert.False(null < @null, "null should not be lower than @null");
        }

        public VersionTests(ITestOutputHelper helper) : base(helper)
        {
        }
    }
}