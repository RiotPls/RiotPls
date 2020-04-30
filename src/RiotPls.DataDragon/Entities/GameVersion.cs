using System;

namespace RiotPls.DataDragon.Entities
{
    public class GameVersion
    {
        private bool IsNewFormat => !Patch.HasValue;
  
        public int Major { get; }
        public int Minor { get; }
        public int? Patch { get; }

        private GameVersion(int major, int minor, int? patch = null)
        {
            Major = major;
            Minor = minor;
            Patch = patch;
        }

        public static GameVersion Parse(string input)
        {
            var start = 0;
            if (input.StartsWith("lolpatch_", StringComparison.Ordinal))
            {
                start = 9;
            }

            var split = input.Split(".");
            if (split.Length != 2 && split.Length != 3)
            {
                throw new InvalidOperationException($"Failed to parse game version \"{input}\". Expected 2 or 3 numbers, got {split.Length}.");
            }
            return new GameVersion( int.Parse(split[0].Substring(start)), int.Parse(split[1]), split.Length > 2 ? int.Parse(split[2]) : (int?) null);
        }
        
        public override string ToString()
        {
            return IsNewFormat 
                ? $"{Major}.{Minor}.{Patch}" 
                : $"lolpatch_{Major}.{Minor}";
        }
    }
}