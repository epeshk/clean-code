using System;
using System.Collections.Generic;
using System.Linq;
using Markdown.Tree;
using Markdown.Utilities;

namespace Markdown.TextParser
{
    public class MarkdownParser : IParser
    {
        public INode ParseSingleParagraph(string paragraph, bool wrapToParagraphNode = false)
        {
            var escaped = new EscapedString(paragraph);
            var nodes = SelectMarkers(escaped).GetNodes(escaped);
            return wrapToParagraphNode ? new ParagraphNode(nodes) : new StructureNode(nodes);
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
            var markers = new IMarker[]
            {
                new ItalicBoldMarker(),
                new BoldMarker(),
                new ItalicMarker()
            };

            return markers
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