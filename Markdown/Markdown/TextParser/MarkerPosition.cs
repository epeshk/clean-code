namespace Markdown.TextParser
{
    internal class MarkerPosition
    {
        public readonly int End;
        public readonly string Marker;
        public readonly int Start;

        public MarkerPosition(int start, int end, string marker)
        {
            Start = start;
            End = end;
            Marker = marker;
        }
    }
}