using System;
using System.Diagnostics.CodeAnalysis;

namespace RiotPls.DataDragon.Entities
{
    /// <summary>
    ///     Represents a game version for League of Legends, like "10.9.1".
    /// </summary>
    public class GameVersion
    {
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
        public int Patch { get; }

        private readonly string _string;

        private GameVersion(int major, int minor, int patch = -1)
        {
            Major = major;
            Minor = minor;
            Patch = patch;

            _string = patch == -1
                ? $"lolpatch_{Major}.{Minor}"
                : $"{Major}.{Minor}.{Patch}";
        }
        
        /// <summary>
        ///     Parses a string input into a <see cref="GameVersion"/>.
        /// </summary>
        /// <param name="input">The game version, in string form.</param>
        /// <returns>A parsed game version object.</returns>
        /// <exception cref="InvalidOperationException">
        ///     Thrown if the provided string does not match the game version format.
        /// </exception>
        public static GameVersion Parse(string input)
            => TryParse(input, out var gameVersion)
            ? gameVersion
            : throw new InvalidOperationException($"Failed to parse game version \"{input}\".");

        /// <summary>
        ///     Parses a string input into a <see cref="GameVersion"/>.
        /// </summary>
        /// <param name="input">The game version, in string form.</param>
        /// <param name="gameVersion">A parsed game version object.</param>
        /// <returns>True if parsing was successful, or false if an error occurred.</returns>
        public static bool TryParse(string input, [MaybeNullWhen(false)] out GameVersion gameVersion)
        {
            gameVersion = null;
            
            ReadOnlySpan<char> inputSpan = input.StartsWith("lolpatch_", StringComparison.Ordinal) ? input.AsSpan(9) : input;

            var major = -1;
            var minor = -1;
            var patch = -1;
            var start = 0;
            var length = inputSpan.Length;

            var currentSpan = inputSpan;

            for (var i = 0; i <= length; i++)
            {
                var finished = i == length;
                if (finished || inputSpan[i] == '.')
                {
                    if (!int.TryParse(currentSpan.Slice(0, i - start), out var result))
                        return false;

                    if (major == -1)
                        major = result;
                    else if (minor == -1)
                        minor = result;
                    else
                    {
                        patch = result;
                        break;
                    }

                    if (finished)
                        break;

                    start = i + 1;
                    currentSpan = inputSpan.Slice(start);
                }
            }

            gameVersion = new GameVersion(major, minor, patch);
            return true;
        }

        public override string ToString() 
            => _string;
    }
}
