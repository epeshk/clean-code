using Markdown.Tree;
using Markdown.Utilities;

namespace Markdown.TextParser
{
    public interface IParser
    {
        INode GetRoot(EscapedString str);
    }
}