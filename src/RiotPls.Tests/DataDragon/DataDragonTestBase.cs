using System;
using RiotPls.DataDragon;

namespace RiotPls.Tests.DataDragon
{
    public class DataDragonTestBase: IDisposable
    {
        protected readonly RiotPls.DataDragon.DataDragonClient _client = new DataDragonClient();

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}