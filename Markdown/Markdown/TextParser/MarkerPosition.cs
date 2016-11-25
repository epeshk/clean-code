using Markdown.TextParser.Markers;

namespace Markdown.TextParser
{
    internal class MarkerPosition
    {
        public readonly IMarker Marker;
        public readonly int Start;
        public readonly int UpperBound;

        public MarkerPosition(int start, int upperBound, IMarker marker)
        {
            Start = start;
            UpperBound = upperBound;
            Marker = marker;
        }
    }
}