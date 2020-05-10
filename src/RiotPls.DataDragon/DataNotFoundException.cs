using System;

namespace RiotPls.DataDragon
{
    [Serializable]
    public sealed class DataNotFoundException : Exception
    {
        public string RequestUrl { get; }

        public DataNotFoundException(string? message, Exception? innerException, string requestUrl)
            : base(message, innerException)
        {
            RequestUrl = requestUrl;
        }
    }
}