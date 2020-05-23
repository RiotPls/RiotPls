using System.Linq;
using System.Threading.Tasks;
using RiotPls.DataDragon.Entities;
using RiotPls.DataDragon.Enums;
using Xunit;
using Xunit.Abstractions;

namespace RiotPls.Tests.DataDragon
{
    public class ItemTests : DataDragonTestBase
    {
        public ItemTests(ITestOutputHelper helper) : base(helper)
        {
        }

        [Fact(DisplayName = "Item API works and deserialization is accurate")]
        public async Task Test_That_ItemsApi_Works()
        {
            var items = await _client.GetItemsAsync(GameVersion.Parse("10.9.1"), Language.French);
            Assert.NotEmpty(items);
        }
    }
}