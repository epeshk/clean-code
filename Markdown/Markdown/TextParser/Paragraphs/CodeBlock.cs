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
            return new CodeBlockNode(RemoveWrapperMarkers(str));
        }

        public string RemoveWrapperMarkers(string str)
        {
            return string.Join("\n", str.Split('\n').Select(s =>
            {
                if (s.StartsWith("    ") && s.Length > 4)
                    return s.Substring(4);
                if (s.StartsWith("\t") && s.Length > 1)
                    return s.Substring(1);
                return string.Empty;
            }));
        }
    }
}