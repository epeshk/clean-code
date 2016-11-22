using System.Collections.Generic;
using System.Linq;
using Markdown.Tree;

namespace Markdown.TextParser.Paragraphs
{
    internal class CodeBlock : IParagraphKind
    {
        public bool IsMatch(string str)
        {
            var lines = str.Split('\n');
            return lines.All(line => line.StartsWith("\t") || line.StartsWith("    "));
        }

        public INode CreateNode(string str, IEnumerable<INode> nodes)
        {
            return new CodeBlockNode(str);
        }

        public string RemoveWrapperMarkers(string str)
        {
            return str;
        }
    }
}