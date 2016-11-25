using System.Linq;
using Markdown.Tree;

namespace Markdown.TextParser.Paragraphs
{
    internal class CodeBlock : IParagraphKind
    {
        public INode ParseOrNull(string str, bool wrap)
        {
            var lines = str.Split('\n');
            if (!lines.All(line => line.StartsWith("\t") || line.StartsWith("    ")))
                return null;
            var escaped = RemoveWrapperMarkers(str);
            return CreateNode(escaped);
        }

        private INode CreateNode(string str)
        {
            return new CodeBlockNode(str);
        }

        private string RemoveWrapperMarkers(string str)
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