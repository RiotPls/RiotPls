namespace RiotPls.DataDragon.Extensions
{
    internal static class StringExtensions
    {
        public static string CapitalizeFirstLetter(this string value)
            => string.Create(value.Length, value, (span, strState) =>
            {
                span[0] = char.ToUpper(strState[0]);
                for (var i = span.Length - 1; i > 0; --i)
                    span[i] = strState[i];
            });
    }
}