using System.Collections.Generic;
using Markdown.Tree;

namespace Markdown.TextParser.Markers
{
    internal class BoldMarker : TagMarker
    {
        public BoldMarker()
            : base("__", true)
        {
        }

        public override INode CreateNode(IEnumerable<INode> nestedNodes)
        {
            return new BoldNode(nestedNodes);
        }
    }
}