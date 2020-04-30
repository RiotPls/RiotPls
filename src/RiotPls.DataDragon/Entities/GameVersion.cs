using System;

namespace RiotPls.DataDragon.Entities
{
    public class GameVersion
    {
        private bool IsNewFormat => !Patch.HasValue;
  
        public int Major { get; }
        public int Minor { get; }
        public int? Patch { get; }

        public GameVersion(int major, int minor, int? patch = null)
        {
            Major = major;
            Minor = minor;
            Patch = patch;
        }
        
        public override string ToString()
        {
            return IsNewFormat 
                ? $"{Major}.{Minor}.{Patch}" 
                : $"lolpatch_{Major}.{Minor}";
        }
    }
}