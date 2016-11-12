using System.Collections.Generic;
using System.Linq;

namespace Markdown.TextParser
{
    internal static class MarkdownEnumerableExtensions
    {
        public static IEnumerable<MarkerPosition> RemoveIntersectsMarkers(
            this IEnumerable<MarkerPosition> markerPositions)
        {
            MarkerPosition current = null;
            foreach (var marker in markerPositions)
            {
                if (current != null && marker.Start >= current.End)
                    current = null;
                if (current == null || (current.End >= marker.End))
                    yield return marker;
                if (current == null)
                    current = marker;
            }
        }

        public static IEnumerable<MarkerPosition> RemoveMarkersNestedIn(
            this IEnumerable<MarkerPosition> markerPositions, params string[] markers)
        {
            MarkerPosition current = null;
            foreach (var marker in markerPositions)
            {
                if (current != null && marker.Start >= current.End)
                    current = null;
                if (current != null)
                    continue;

                yield return marker;
                if (markers.Contains(marker.Marker))
                    current = marker;
            }
        }
    }
}