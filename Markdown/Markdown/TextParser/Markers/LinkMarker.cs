using System;
using Markdown.Tree;
using Markdown.Utilities;

namespace Markdown.TextParser.Markers
{
    internal class LinkMarker : TagMarker
    {
        public LinkMarker()
            : base("(", "]", false)
        {
        }

        public override INode CreateNode(string s, int start, int end)
        {
            var parts = s.Substring(start, end-start-1).Trim('(',']').Split(new [] {")["}, StringSplitOptions.RemoveEmptyEntries);
            if(parts.Length >= 2)
                return new LinkNode(parts[0], parts[1]);
            if(parts.Length == 1)
                return new LinkNode(parts[0]);
            return new TextNode(string.Empty);
        }

        public override bool MatchEnd(EscapedString s, int position)
        {
            return s.MatchEnd(position, "]") ||
                   (s.MatchEnd(position, ")") && s.OnChar(position + 1, c => c != '[', true));
        }
    }
}