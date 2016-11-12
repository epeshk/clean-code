namespace Markdown.TextRender
{
    internal class Position
    {
        public readonly int Index;
        public readonly bool IsStart;
        public readonly TagDescription TagDescription;

        public Position(int index, bool isStart, TagDescription tagDescription)
        {
            Index = index;
            IsStart = isStart;
            TagDescription = tagDescription;
        }
    }
}