namespace Markdown.Utilities
{
    internal static class EscapedStringExtensions
    {
        public static bool MatchStart(this EscapedString paragraph, int position, string marker)
        {
            return Match(paragraph, position, marker)
                   && !CharIsWhitespace(paragraph, position + marker.Length);
        }

        public static bool MatchEnd(this EscapedString paragraph, int position, string marker)
        {
            return Match(paragraph, position, marker)
                   && !CharIsWhitespace(paragraph, position - 1);
        }

        private static bool Match(EscapedString paragraph, int position, string marker)
        {
            return paragraph.EqualsFromPosition(marker, position)
                   && HasGoodBoundary(paragraph, marker, position);
        }

        private static bool CharIsWhitespace(EscapedString str, int position)
        {
            return str.OnChar(position, char.IsWhiteSpace, false);
        }

        private static bool EqualsFromPosition(this EscapedString str, string other, int position)
        {
            if (str.Length - position < other.Length)
                return false;
            for (var i = 0; i < other.Length; i++)
                if (str[position + i] != other[i] || str.IsEscaped(position + i))
                    return false;
            return true;
        }

        private static bool HasGoodBoundary(EscapedString paragraph, string marker, int position)
        {
            if (paragraph.OnChar(position - 1, c => char.IsDigit(c) || c == '_', false))
                return false;
            return paragraph.OnChar(position + marker.Length, c => !(char.IsDigit(c) || c == '_'), true);
        }
    }
}