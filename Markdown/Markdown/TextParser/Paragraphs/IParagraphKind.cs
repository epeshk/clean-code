using JetBrains.Annotations;
using Markdown.Tree;

namespace Markdown.TextParser.Paragraphs
{
    internal interface IParagraphKind
    {
        [CanBeNull]
        INode Parse(string str, bool wrap);
    }
}