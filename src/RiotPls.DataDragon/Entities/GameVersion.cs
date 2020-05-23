using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using RiotPls.DataDragon.Converters;

namespace RiotPls.DataDragon.Entities
{
    /// <summary>
    ///     Represents a game version for League of Legends, like "10.9.1".
    /// </summary>
    [JsonConverter(typeof(GameVersionJsonConverter))]
    public sealed class GameVersion : IEquatable<GameVersion>, IComparable<GameVersion>
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

        private readonly string _version;

        private GameVersion(int major, int minor, int patch = -1)
        {
            Major = major;
            Minor = minor;
            Patch = patch;

            _version = patch == -1
                ? $"lolpatch_{Major}.{Minor}"
                : $"{Major}.{Minor}.{Patch}";
        }

        /// <summary>
        ///     Parses a string input into a <see cref="GameVersion"/>.
        /// </summary>
        /// <param name="input">
        ///     The game version, in string form.
        /// </param>
        /// <returns>A parsed game version object.</returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="input"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="FormatException">
        ///     Thrown when <paramref name="input"/> does not have a valid format.
        /// </exception>
        public static GameVersion Parse(string input)
        {
            if (input is null)
                throw new ArgumentNullException(nameof(input));

            if (TryParse(input, out var version))
                return version;

            throw new FormatException($"The input string was not in a valid format. input: {input}");
        }

        /// <summary>
        ///     Parses a string input into a <see cref="GameVersion"/>.
        /// </summary>
        /// <param name="input">
        ///     The game version, in string form.
        ///     </param>
        /// <param name="gameVersion">
        ///     A parsed game version object.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if parsing was successful, otherwise <see langword="false"/>.
        /// </returns>
        public static bool TryParse(string input, [NotNullWhen(true)] out GameVersion? gameVersion)
        {
            gameVersion = null;

            if (string.IsNullOrWhiteSpace(input))
                return false;

            ReadOnlySpan<char> inputSpan =
                input.StartsWith("lolpatch_", StringComparison.Ordinal) ? input.AsSpan(9) : input;

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
                    if (!int.TryParse(currentSpan.Slice(0, i - start), out var result) || result < 0)
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
            => _version;

        public override bool Equals(object? obj)
            => Equals(obj as GameVersion);

        public override int GetHashCode()
            => HashCode.Combine(Major, Minor, Patch);

        /// <inheritdoc/>
        public bool Equals(GameVersion? other)
            => other is object
            && Major == other.Major
            && Minor == other.Minor
            && Patch == other.Patch;

        /// <inheritdoc/>
        public int CompareTo(GameVersion? other)
        {
            if (other is null)
                return 1;

            if (Major > other.Major)
                return 1;

            if (Major < other.Major)
                return -1;

            if (Minor > other.Minor)
                return 1;

            if (Minor < other.Minor)
                return -1;

            if (Patch > other.Patch)
                return 1;

            if (Patch < other.Patch)
                return -1;

            return 0;
        }

        /// <summary>
        ///     Indicates if both objects of type <see cref="GameVersion"/> are equal.
        /// </summary>
        /// <param name="left">
        ///     The left-side input to compare.
        /// </param>
        /// <param name="right">
        ///     The right-side input to compare.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the objects are equal. Otherwise <see langword="false"/>.
        /// </returns>
        public static bool operator ==(GameVersion? left, GameVersion? right)
        {
            if (left is null)
                return right is null;

            return left.Equals(right);
        }

        /// <summary>
        ///     Indicates if both objects of type <see cref="GameVersion"/> are not equal.
        /// </summary>
        /// <param name="left">
        ///     The left-side input to compare.
        /// </param>
        /// <param name="right">
        ///     The right-side input to compare.
        /// </param>
        /// <returns>
        ///     <see langword="false"/> if the objects are equal. Otherwise <see langword="true"/>.
        /// </returns>
        public static bool operator !=(GameVersion? left, GameVersion? right)
        {
            if (left is null)
                return right is object;

            return !left.Equals(right);
        }

        /// <summary>
        ///     Indicates if the left objects of type <see cref="GameVersion"/> is higher than the right one.
        /// </summary>
        /// <param name="left">
        ///     The left-side input to compare.
        /// </param>
        /// <param name="right">
        ///     The right-side input to compare.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the left object is higher than the right one. Otherwise <see langword="false"/>.
        /// </returns>
        public static bool operator >(GameVersion? left, GameVersion? right)
        {
            if (right is null)
                return left is object;

            return right.CompareTo(left) < 0;
        }

        /// <summary>
        ///     Indicates if the left objects of type <see cref="GameVersion"/> is lower than the right one.
        /// </summary>
        /// <param name="left">
        ///     The left-side input to compare.
        /// </param>
        /// <param name="right">
        ///     The right-side input to compare.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the left object is lower than the right one. Otherwise <see langword="false"/>.
        /// </returns>
        public static bool operator <(GameVersion? left, GameVersion? right)
        {
            if (left is null)
                return right is object;

            return left.CompareTo(right) < 0;
        }

        /// <summary>
        ///     Indicates if the left objects of type <see cref="GameVersion"/> is higher than or equal to the right one.
        /// </summary>
        /// <param name="left">
        ///     The left-side input to compare.
        /// </param>
        /// <param name="right">
        ///     The right-side input to compare.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the left object is higher than or equal to the right one. Otherwise <see langword="false"/>.
        /// </returns>
        public static bool operator >=(GameVersion? left, GameVersion? right)
            => right is null || right.CompareTo(left) <= 0;

        /// <summary>
        ///     Indicates if the left objects of type <see cref="GameVersion"/> is lower than or equal to the right one.
        /// </summary>
        /// <param name="left">
        ///     The left-side input to compare.
        /// </param>
        /// <param name="right">
        ///     The right-side input to compare.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the left object is lower than or equal to the right one. Otherwise <see langword="false"/>.
        /// </returns>
        public static bool operator <=(GameVersion? left, GameVersion? right)
            => left is null || left.CompareTo(right) <= 0;
    }
}