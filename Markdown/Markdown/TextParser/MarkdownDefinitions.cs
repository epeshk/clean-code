using Markdown.TextParser.Markers;
using Markdown.TextParser.Paragraphs;

namespace Markdown.TextParser
{
    internal static class MarkdownDefinitions
    {
        public static readonly IMarker[] Markers =
        {
            new ItalicBoldMarker(),
            new BoldMarker(),
            new LinkMarker(),
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
}