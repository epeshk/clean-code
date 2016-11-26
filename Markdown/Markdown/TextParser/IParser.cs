using Markdown.Tree;

namespace Markdown.TextParser
{
    public interface IParser
    {
        INode ParseSingleParagraph(string paragraph, bool wrap = false);
        INode ParseText(string text, bool wrap = true);
    }
}