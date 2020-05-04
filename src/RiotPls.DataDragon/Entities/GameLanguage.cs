using System;
using System.Diagnostics.CodeAnalysis;

namespace RiotPls.DataDragon.Entities
{
    /// <summary>
    ///     Represents a game language for League of Legends,
    ///     like "en_US", expressed as UTF-8 culture codes.
    /// </summary>
    public class GameLanguage : IEquatable<GameLanguage>, IEquatable<string>  
    {
        /// <summary>
        ///     Game language code.
        /// </summary>
        public string Code { get; }
        
        private GameLanguage(string code)
        {
            Code = code;
        }

        /// <summary>
        ///     Parses a string input into a <see cref="GameLanguage"/>.
        /// </summary>
        /// <param name="input">The game language, in string form.</param>
        /// <returns>A parsed game language object.</returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if <paramref name="input"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="FormatException">
        ///     Thrown if <paramref name="input"/> does not have a valid format.
        /// </exception>
        public static GameLanguage Parse(string input)
        {
            if (input is null)
                throw new ArgumentNullException(nameof(input));

            if (TryParse(input, out var language))
                return language;
            
            throw new FormatException(
                $"The input string was not in a valid format. input: {input}");
        }

        /// <summary>
        ///     Parses a string input into a <see cref="GameLanguage"/>.
        /// </summary>
        /// <param name="input">The game language, in string form.</param>
        /// <param name="gameLanguage">A parsed game language object.</param>
        /// <returns>True if parsing was successful, or false if an error occured.</returns>
        public static bool TryParse(string input, 
            [NotNullWhen(true)] out GameLanguage? gameLanguage)
        {
            gameLanguage = null;

            if (input is null)
                return false;

            if (input.Length != 5)
                return false;

            if (input[2] != '_')
                return false;

            for (var i = 0; i < input.Length; ++i)
                if (i != 2 && !char.IsLetter(input[i]))
                    return false;

            var validCode = string.Create(5, input, (span, state) =>
            {
                span[0] = char.ToLower(state[0]);
                span[1] = char.ToLower(state[1]);
                span[2] = '_';
                span[3] = char.ToUpper(state[3]);
                span[4] = char.ToUpper(state[4]);
            });
            
            gameLanguage = new GameLanguage(validCode);
            return true;
        }

        public override string ToString()
            => Code;

        public override bool Equals(object? obj)
            => Equals(obj as GameLanguage);

        public override int GetHashCode()
            => HashCode.Combine(Code);

        /// <inheritdoc/>
        public bool Equals(GameLanguage? other)
            => other is GameLanguage language
               && language.Code == Code;

        /// <inheritdoc/>
        public bool Equals(string? other)
            => other is string language
               && language == Code;

        /// <summary>
        ///     Implicitly parse a string into a <see cref="GameLanguage"/>.
        /// </summary>
        /// <param name="input">The game language, in string form.</param>
        public static implicit operator GameLanguage(string input)
            => TryParse(input, out var language)
                ? language
                : throw new InvalidCastException(
                    $"Cannot convert the input string to a GameLanguage. input: {input}");
    }
}