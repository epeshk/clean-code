using System;
using System.Collections.Generic;
using System.Linq;
using Markdown.Tree;
using Markdown.Utilities;

namespace Markdown.TextParser
{
    public class MarkdownParser : IParser
    {
        private static readonly IMarker[] Markers = {
            new ItalicBoldMarker(),
            new BoldMarker(),
            new ItalicMarker(),
            new InlineCodeMarker()
        };

        private static readonly IParagraphKind[] ParagraphKinds =
        {
            new Header(),
            new CodeBlock(),
            new SimpleParagraph()
        };

        public INode ParseSingleParagraph(string paragraph, bool wrap = false)
        {
            var kind = ParagraphKinds.First(k => k.IsMatch(paragraph));
            var escaped = new EscapedString(kind.RemoveWrapperMarkers(paragraph));
            var nodes = SelectMarkers(escaped).GetNodes(escaped);
            return wrap ? kind.CreateNode(paragraph, nodes) : new StructureNode(nodes) ;
        }

        public INode ParseText(string text)
        {
            return new StructureNode(SplitToParagraphs(text).Select(p => ParseSingleParagraph(p, true)));
        }

        internal IEnumerable<MarkerPosition> GetMarkersPosition(EscapedString paragraph, IMarker marker)
        {
            var startCaptured = false;
            var startPosition = -1;
            for (var position = 0; position < paragraph.Length; ++position)
            {
                if (paragraph.IsEscaped(position))
                    continue;
                if (startCaptured && marker.MatchEnd(paragraph, position))
                {
                    yield return new MarkerPosition(startPosition, position + marker.AddOnEnd, marker);
                    startCaptured = false;
                }
                else if (marker.MatchStart(paragraph, position))
                {
                    startCaptured = true;
                    startPosition = position;
                }
            }
        }

        private IEnumerable<MarkerPosition> SelectMarkers(EscapedString paragraph)
        {

            return Markers
                .SelectMany(marker => GetMarkersPosition(paragraph, marker))
                .OrderBy(x => x.Start)
                .RemoveIntersectsMarkers()
                .RemoveRedundantNestedMarkers();
        }

        private string[] SplitToParagraphs(string text)
        {
            return text
                .Replace("\r\n", "\n")
                .Split(new[] {"\n\n"}, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}