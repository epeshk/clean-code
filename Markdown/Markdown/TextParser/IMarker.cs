using System.Collections.Generic;
using Markdown.Tree;
using Markdown.Utilities;

namespace Markdown.TextParser
{
    internal interface IMarker
    {
        bool MatchStart(EscapedString s, int position);
        bool MatchEnd(EscapedString s, int position);
        bool CanContainNested { get; }
        int AddOnStart { get; }
        int AddOnEnd { get; }
        INode CreateNode(IEnumerable<INode> nestedNodes);
        INode CreateNode(string s, int start, int end);
    }
}