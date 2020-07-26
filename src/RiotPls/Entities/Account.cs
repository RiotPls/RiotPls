using RiotPls.Entities.DataObject;

namespace RiotPls.Entities
{
    public sealed class Account
    {
        private readonly LeagueClient _client;
        
        public string Puuid { get; }
        public string GameName { get; }
        public string TagLine { get; }

        internal Account(LeagueClient client, AccountDto dto)
        {
            _client = client;
            
            Puuid = dto.Puuid;
            GameName = dto.GameName;
            TagLine = dto.TagLine;
        }
    }
}