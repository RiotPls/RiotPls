using System;

namespace RiotPls.DataDragon.Entities
{
    /// <summary>
    /// Represents a game version for League of Legends, like "10.9.1".
    /// </summary>
    public class GameVersion
    {
        private bool IsNewFormat => !Patch.HasValue;
  
        /// <summary>
        ///     The major release version.
        /// </summary>
        public int Major { get; }
        
        /// <summary>
        ///     The minor release version.
        /// </summary>
        public int Minor { get; }
        
        /// <summary>
        ///     The patch release version.
        /// </summary>
        public int? Patch { get; }

        private GameVersion(int major, int minor, int? patch = null)
        {
            Major = major;
            Minor = minor;
            Patch = patch;
        }

        /// <summary>
        ///     Parses a string input into a <see cref="GameVersion"/>.
        /// </summary>
        /// <param name="input">The game version, in string form.</param>
        /// <returns>A parsed game version object.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the provided string does not match the game version format.</exception>
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