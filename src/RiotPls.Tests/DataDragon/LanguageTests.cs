using System;
using System.Linq;
using System.Threading.Tasks;
using RiotPls.DataDragon;
using RiotPls.DataDragon.Entities;
using Xunit;

namespace RiotPls.Tests.DataDragon
{
    public class LanguageTests : DataDragonTestBase
    {
        [Fact(DisplayName = "Language API works and deserialization is accurate")]
        public async Task Test_That_LanguageApi_Works()
        {
            var languages = await _client.GetLanguagesAsync();
            Assert.True(languages.Count > 10,
                $"Expected there to be at least 10 languages in response, got {languages.Count}");
            Assert.True(languages.Contains("en_US"), "Expected languages response to contain en_US.");
            Assert.True(languages.Contains("de_DE"), "Expected languages response to contain de_DE.");
        }
    }
}