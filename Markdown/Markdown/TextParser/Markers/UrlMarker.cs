using Markdown.Tree;

namespace Markdown.TextParser.Markers
{
    internal class UrlMarker : TagMarker
    {
        public UrlMarker()
            : base("(", ")", false)
        {
        }

        public override INode CreateNode(string s, int start, int end)
        {
            return new LinkNode(s.Substring(start, end).Trim('(', ')'));
        }
    }
}