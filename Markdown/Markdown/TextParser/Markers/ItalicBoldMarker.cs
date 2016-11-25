using Markdown.Tree;

namespace Markdown.TextParser.Markers
{
    internal class ItalicBoldMarker : TagMarker
    {
        public ItalicBoldMarker()
            : base("___", false)
        {
        }

        public override INode CreateNode(string s, int start, int end)
        {
            return new BoldNode(new[] {new ItalicNode(s.Substring(start + 3, end - start - 6))});
        }
    }
}