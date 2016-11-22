using Markdown.Tree;

namespace Markdown.TextParser.Markers
{
    internal class ItalicMarker : TagMarker
    {
        public ItalicMarker()
            : base("_", false)
        {
        }

        public override INode CreateNode(string s, int start, int end)
        {
            return new ItalicNode(s.Substring(start+1, end-start-2));
        }
    }
}