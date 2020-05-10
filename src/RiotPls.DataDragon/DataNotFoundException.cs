using System;
using RiotPls.DataDragon.Entities;
using RiotPls.DataDragon.Enums;

namespace RiotPls.DataDragon
{
    [Serializable]
    public sealed class DataNotFoundException : Exception
    {
        public string RequestUrl { get; }
        
        public GameVersion Version { get; }
        
        public Language Language { get; }

        public DataNotFoundException(string? message, Exception? innerException, string requestUrl, 
            GameVersion version, Language language)
            : base(message, innerException)
        {
            RequestUrl = requestUrl;
            Version = version;
            Language = language;
        }
    }
}