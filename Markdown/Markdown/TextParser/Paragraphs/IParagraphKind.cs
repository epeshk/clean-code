using Markdown.Tree;

namespace Markdown.TextParser.Paragraphs
{
    internal interface IParagraphKind
    {
        INode ParseOrNull(string str, bool wrap);
    }
}