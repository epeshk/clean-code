using System.Collections.Generic;
using System.Linq;
using Markdown.TextRender;
using Markdown.Utilities;

namespace Markdown.TextParser
{
    public class MarkdownParser : IParser
    {
        public IEnumerable<TagDescription> GetTagDescriptions(EscapedString paragraph)
        {
            var italicBoldMarkers = GetMarkersPosition(paragraph, "___");
            var boldMarkers = GetMarkersPosition(paragraph, "__");
            var italicMarkers = GetMarkersPosition(paragraph, "_");

            var markdownMarkers = italicBoldMarkers
                .Concat(boldMarkers)
                .Concat(italicMarkers)
                .OrderBy(x => x.Start)
                .RemoveIntersectsMarkers()
                .RemoveMarkersNestedIn("_", "___");

            return markdownMarkers.SelectMany(GetRenderMarkers);
        }

        internal IEnumerable<MarkerPosition> GetMarkersPosition(EscapedString paragraph, string marker)
        {
            var startCaptured = false;
            var startPosition = -1;
            for (var position = 0; position < paragraph.Length; ++position)
            {
                if (paragraph.IsEscaped(position))
                    continue;
                if (startCaptured && paragraph.MatchEnd(position, marker))
                {
                    yield return new MarkerPosition(startPosition, position, marker);
                    startCaptured = false;
                }
                else if (paragraph.MatchStart(position, marker))
                {
                    startCaptured = true;
                    startPosition = position;
                }
            }
        }

        private IEnumerable<TagDescription> GetRenderMarkers(MarkerPosition marker)
        {
            switch (marker.Marker)
            {
                case "_":
                    yield return
                        new TagDescription(Tag.Italic, marker.Start, marker.End, 1, 1);
                    break;
                case "__":
                    yield return
                        new TagDescription(Tag.Bold, marker.Start, marker.End, 2, 2);
                    break;
                case "___":
                    yield return
                        new TagDescription(Tag.Bold, marker.Start, marker.End, 2, 2);
                    yield return
                        new TagDescription(Tag.Italic, marker.Start, marker.End, 1, 1);
                    break;
            }
        }
    }
}