using System.Collections.Generic;
using Markdown.Tree;
using Markdown.Utilities;

namespace Markdown.TextParser.Markers
{
    internal interface IMarker
    {
        bool MatchStart(EscapedString s, int position);
        bool MatchEnd(EscapedString s, int position);
        bool CanBeJoined(IMarker other, bool otherInLeft);
        bool CanContainNested { get; }
        int AddOnStart { get; }
        int AddOnEnd { get; }
        INode CreateNode(IEnumerable<INode> nestedNodes);
        INode CreateNode(string s, int start, int end);
    }
}