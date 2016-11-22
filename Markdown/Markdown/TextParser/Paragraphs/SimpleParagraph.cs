using System.Collections.Generic;
using Markdown.Tree;

namespace Markdown.TextParser.Paragraphs
{
    internal class SimpleParagraph : IParagraphKind
    {
        public bool IsMatch(string str)
        {
            return true;
        }

        public INode CreateNode(string str, IEnumerable<INode> nodes)
        {
            return new ParagraphNode(nodes);
        }

        public string RemoveWrapperMarkers(string str)
        {
            return str;
        }
    }
}