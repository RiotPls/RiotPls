using System;
using RiotPls.DataDragon;
using RiotPls.DataDragon.Enums;
using Xunit.Abstractions;

namespace RiotPls.Tests.DataDragon
{
    public class DataDragonTestBase : IDisposable
    {
        protected readonly DataDragonClient _client = new DataDragonClient(new DataDragonClientOptions { CacheMode = CacheMode.KeepAll });
        protected readonly ITestOutputHelper _output;

        public DataDragonTestBase(ITestOutputHelper helper)
        {
            _output = helper;
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}