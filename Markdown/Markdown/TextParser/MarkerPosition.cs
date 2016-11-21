namespace Markdown.TextParser
{
    internal class MarkerPosition
    {
        public readonly int UpperBound;
        public readonly IMarker Marker;
        public readonly int Start;

        public MarkerPosition(int start, int upperBound, IMarker marker)
        {
            Start = start;
            UpperBound = upperBound;
            Marker = marker;
        }
    }
}