using System;

namespace RiotPls.DataDragon
{
    [Serializable]
    public sealed class DataNotFoundException : Exception
    {
        public DataNotFoundException() : base()
        {
        }

        public DataNotFoundException(string? message) : base(message)
        {
        }

        public DataNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}