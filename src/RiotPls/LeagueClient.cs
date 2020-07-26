using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using RiotPls.DataDragon;
using RiotPls.DataDragon.Entities;
using RiotPls.DataDragon.Enums;
using RiotPls.Entities;

namespace RiotPls
{
    public class LeagueClient
    {
        private readonly IDataDragonClient _dataDragonClient;

        public LeagueClient(IDataDragonClient dataDragonClient)
        {
            _dataDragonClient = dataDragonClient ?? throw new ArgumentNullException(nameof(dataDragonClient));
        }

        public async Task<DataDragonDataContainer?> GetLatestStaticDataAsync(Language? language = null, CancellationToken token = default)
        {
            var version = await _dataDragonClient.FetchLatestVersionAsync(token)
                .ConfigureAwait(false);

            if (version is null)
                return null;

            return await DataDragonDataContainer.FetchAsync(_dataDragonClient, version, language, token);
        }
    }
}