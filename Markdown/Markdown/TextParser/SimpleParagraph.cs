using System.Collections.Generic;
using Markdown.Tree;

namespace Markdown.TextParser
{
    internal class SimpleParagraph : IParagraphKind
    {
        public bool IsMatch(string str)
        {
            return true;
        }

        public StructureNode CreateNode(string str, IEnumerable<INode> nodes)
        {
            return new ParagraphNode(nodes);
        }

        public string RemoveWrapperMarkers(string str)
        {
            return str;
        }
    }
}