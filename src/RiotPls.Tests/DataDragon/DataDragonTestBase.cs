using System;
using RiotPls.DataDragon;
using Xunit.Abstractions;

namespace RiotPls.Tests.DataDragon
{
    public class DataDragonTestBase: IDisposable
    {
        protected readonly RiotPls.DataDragon.DataDragonClient _client = new DataDragonClient();
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