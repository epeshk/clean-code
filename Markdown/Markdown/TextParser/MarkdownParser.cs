using System;
using System.Linq;
using Markdown.Tree;

namespace Markdown.TextParser
{
    public class MarkdownParser : IParser
    {
        public INode ParseSingleParagraph(string paragraph, bool wrap = false)
        {
            return MarkdownDefinitions.ParagraphKinds
                .Select(k => k.ParseOrNull(paragraph, wrap))
                .First(x => x != null);
        }

        public INode ParseText(string text, bool wrap = true)
        {
            return new StructureNode(SplitToParagraphs(text).Select(p => ParseSingleParagraph(p, wrap)));
        }


        private string[] SplitToParagraphs(string text)
        {
            return text
                .Replace("\r\n", "\n")
                .Split(new[] {"\n\n"}, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}