namespace Markdown.TextRender
{
    public class TagDescription
    {
        public readonly int End;
        public readonly int SkipOnEnd;
        public readonly int SkipOnStart;
        public readonly int Start;
        public readonly Tag Tag;

        public TagDescription(Tag tag, int start, int end, int skipOnStart, int skipOnEnd)
        {
            Tag = tag;
            SkipOnStart = skipOnStart;
            SkipOnEnd = skipOnEnd;
            Start = start;
            End = end;
        }
    }
}