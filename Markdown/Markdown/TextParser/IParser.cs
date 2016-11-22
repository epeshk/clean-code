using Markdown.Tree;

namespace Markdown.TextParser
{
    public interface IParser
    {
        INode ParseSingleParagraph(string paragraph, bool wrapToParagraphNode = false);
        INode ParseText(string text);
    }
}