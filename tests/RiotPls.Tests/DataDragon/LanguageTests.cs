using System.Linq;
using System.Threading.Tasks;
using RiotPls.DataDragon.Enums;
using Xunit;
using Xunit.Abstractions;

namespace RiotPls.Tests.DataDragon
{
    public class LanguageTests : DataDragonTestBase
    {
        public LanguageTests(ITestOutputHelper helper) : base(helper)
        {
        }

        [Fact(DisplayName = "Language API works and deserialization is accurate")]
        public async Task Test_That_LanguageApi_Works()
        {
            var languages = await _client.GetLanguagesAsync();
            Assert.True(languages.Count > 10,
                $"Expected there to be at least 10 languages in response, got {languages.Count}");
            Assert.True(languages.Contains(Language.AmericanEnglish),
                "Expected languages response to contain en_US.");
            Assert.True(languages.Contains(Language.German),
                "Expected languages response to contain de_DE.");
        }
    }
}