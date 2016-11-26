using System;
using Markdown.Tree;
using Markdown.Utilities;

namespace Markdown.TextParser.Markers
{
    internal class LinkMarker : TagMarker
    {
        public LinkMarker()
            : base("[", ")", false)
        {
        }

        public override INode CreateNode(string s, int start, int end)
        {
            if (end - start < 2)
                return new TextNode(s.Substring(start, end - start));
            var parts = s.Substring(start + 1, end - start - 2)
                .Split(new[] {"]("}, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 2)
                return new LinkNode(parts[1], new StructureNode(new EscapedString(parts[0]).GetNodes()));
            return new TextNode(s.Substring(start, end - start));
        }
    }
}