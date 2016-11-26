using System;
using System.Linq;
using Markdown.Tree;
using Markdown.Utilities;

namespace Markdown.TextParser.Paragraphs
{
    internal class NumberedList : IParagraphKind
    {
        public INode ParseOrNull(string str, bool wrap)
        {
            var lines =
                str.Split('\n')
                    .Select(line => line.Split(new[] {".  "}, StringSplitOptions.RemoveEmptyEntries))
                    .ToList();
            if (!lines.All(s => s.Length > 1 && s[0].IsNumber()))
                return null;
            return new ListNode(lines.Select(line => new ListEntryNode(new EscapedString(line[1]).GetNodes())));
        }
    }
}