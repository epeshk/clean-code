using System.Collections.Generic;
using Markdown.Nodes;

namespace Markdown.TextParser
{
    public interface IParser
    {
        IEnumerable<INode> ParseToNodes(string str);
    }
}