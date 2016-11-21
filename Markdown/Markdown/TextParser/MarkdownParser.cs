using System.Collections.Generic;
using System.Linq;
using Markdown.Tree;
using Markdown.Utilities;

namespace Markdown.TextParser
{
    class MarkdownParser : IParser
    {
        public IEnumerable<MarkerPosition> SelectMarkers(EscapedString paragraph)
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

        public INode GetRoot(EscapedString str)
        {
            return new StructureNode(SelectMarkers(str).GetNodes(str));
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
                    yield return new MarkerPosition(startPosition, position+marker.AddOnEnd, marker);
                    startCaptured = false;
                }
                else if (marker.MatchStart(paragraph, position))
                {
                    startCaptured = true;
                    startPosition = position;
                }
            }
        }
    }
}