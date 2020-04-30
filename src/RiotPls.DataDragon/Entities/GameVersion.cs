using System;

namespace RiotPls.DataDragon.Entities
{
    /// <summary>
    /// Represents a game version for League of Legends, like "10.9.1".
    /// </summary>
    public class GameVersion
    {
        /// <summary>
        ///     The major release version.
        /// </summary>
        public readonly int Major;

        /// <summary>
        ///     The minor release version.
        /// </summary>
        public readonly int Minor;

        /// <summary>
        ///     The patch release version.
        /// </summary>
        public readonly int Patch;

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
        /// <exception cref="InvalidOperationException">Thrown if the provided string does not match the game version format.</exception>
        public static GameVersion Parse(string input)
        {
            ReadOnlySpan<char> inputSpan;
            if (input.StartsWith("lolpatch_", StringComparison.Ordinal))
                inputSpan = input.AsSpan().Slice(9);
            else
                inputSpan = input;

            int major = -1;
            int minor = -1;
            int patch = -1;
            int start = 0;
            int length = inputSpan.Length;

            ReadOnlySpan<char> currentSpan = inputSpan;

            for (int i = 0; i <= length; i++)
            {
                bool finished = i == length;
                if (finished || inputSpan[i] == '.')
                {
                    if(!int.TryParse(currentSpan.Slice(0, i - start), out int result))
                        throw new InvalidOperationException($"Failed to parse game version \"{input}\".");

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

            return new GameVersion(major, minor, patch);
        }

        public override string ToString() => _string;
    }
}
