using System.Collections.Generic;
using System.Linq;
using Markdown.TextParser;
using Markdown.TextParser.Markers;

namespace Markdown.Utilities
{
    internal static class EscapedStringExtensions
    {
        public static bool MatchStart(this EscapedString paragraph, int position, string marker)
        {
            if (position < 0)
                return false;
            return Match(paragraph, position, marker)
                   && !CharIsWhitespace(paragraph, position + marker.Length);
        }

        public static bool MatchEnd(this EscapedString paragraph, int position, string marker)
        {
            if (position < 0)
                return false;
            return Match(paragraph, position, marker)
                   && !CharIsWhitespace(paragraph, position - 1);
        }

        public static IEnumerable<MarkerPosition> SelectMarkers(this EscapedString paragraph)
        {
            return MarkdownDefinitions.Markers
                .SelectMany(paragraph.GetMarkersPosition)
                .OrderBy(x => x.Start)
                .RemoveIntersectsMarkers()
                .RemoveRedundantNestedMarkers();
        }

        public static IEnumerable<MarkerPosition> GetMarkersPosition(this EscapedString paragraph, IMarker marker)
        {
            var startCaptured = false;
            var startPosition = -1;
            for (var position = 0; position < paragraph.Length; ++position)
            {
                if (paragraph.IsEscaped(position))
                    continue;
                if (startCaptured && marker.MatchEnd(paragraph, position))
                {
                    yield return new MarkerPosition(startPosition, position + marker.AddOnEnd, marker);
                    startCaptured = false;
                }
                else if (marker.MatchStart(paragraph, position))
                {
                    startCaptured = true;
                    startPosition = position;
                }
            }
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
            if (paragraph.OnChar(position - 1, c => char.IsDigit(c) || c == marker[0], false))
                return false;
            return paragraph.OnChar(position + marker.Length, c => !(char.IsDigit(c) || c == marker[0]), true);
        }
    }
}