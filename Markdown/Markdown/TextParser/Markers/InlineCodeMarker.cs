using Markdown.Tree;

namespace Markdown.TextParser.Markers
{
    internal class InlineCodeMarker : TagMarker
    {
        public InlineCodeMarker()
            : base("`", false)
        {
        }

        public override INode CreateNode(string s, int start, int end)
        {
            return new InlineCodeNode(s.Substring(start+1, end-start-2));
        }
    }
}