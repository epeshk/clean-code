using System;
using System.Collections.Generic;
using System.Linq;
using Markdown.TextParser.Markers;
using Markdown.TextParser.Paragraphs;
using Markdown.Tree;
using Markdown.Utilities;

namespace Markdown.TextParser
{
    internal static class MarkdownDefinitions
    {
        public static readonly IMarker[] Markers = {
            new ItalicBoldMarker(),
            new BoldMarker(),
            new ItalicMarker(),
            new InlineCodeMarker()
        };

        public static readonly IParagraphKind[] ParagraphKinds =
        {
            new Header(),
            new CodeBlock(),
            new SimpleParagraph()
        };
    }
    public class MarkdownParser : IParser
    {
        

        public INode ParseSingleParagraph(string paragraph, bool wrap = false)
        {
            return MarkdownDefinitions.ParagraphKinds
                .Select(k => k.ParseOrNull(paragraph, wrap))
                .First(x => x != null);
        }

        public INode ParseText(string text)
        {
            return new StructureNode(SplitToParagraphs(text).Select(p => ParseSingleParagraph(p, true)));
        }


        private string[] SplitToParagraphs(string text)
        {
            return text
                .Replace("\r\n", "\n")
                .Split(new[] {"\n\n"}, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}