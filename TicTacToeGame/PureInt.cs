using System;
using System.Globalization;

namespace TicTacToeGame
{
    public static class PureInt
    {
        [AssumeIsPure]
        public static bool TryParse(
            this string s,
            NumberStyles style,
            IFormatProvider provider,
            out int result)
        {
            if (provider == null)
                throw new Exception($"{nameof(provider)} cannot be null");

            return int.TryParse(s, style, provider, out result);
        }

        public static bool TryParseCultureInvariant(
            this string s,
            out int result)
        {
            return TryParse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out result);
        }
    }
}