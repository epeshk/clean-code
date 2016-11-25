using Markdown.Tree;
using Markdown.Utilities;

namespace Markdown.TextParser.Paragraphs
{
    internal class SimpleParagraph : IParagraphKind
    {
        public INode ParseOrNull(string str, bool wrap)
        {
            var escaped = new EscapedString(str);
            return !wrap ? new StructureNode(escaped.GetNodes()) : new ParagraphNode(escaped.GetNodes());
        }
    }
}