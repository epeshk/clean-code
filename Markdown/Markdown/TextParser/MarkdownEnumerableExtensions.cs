using System.Collections.Generic;
using Markdown.Tree;
using Markdown.Utilities;

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
                if (current != null && marker.Start >= current.UpperBound)
                    current = null;
                if (current == null || (current.UpperBound >= marker.UpperBound))
                    yield return marker;
                if (current == null)
                    current = marker;
            }
        }

        public static IEnumerable<MarkerPosition> RemoveRedundantNestedMarkers(
            this IEnumerable<MarkerPosition> markerPositions)
        {
            MarkerPosition current = null;
            foreach (var marker in markerPositions)
            {
                if (current != null && marker.Start >= current.UpperBound)
                    current = null;
                if (current != null)
                    continue;

                yield return marker;
                if (!marker.Marker.CanContainNested)
                    current = marker;
            }
        }

        public static IEnumerable<INode> GetNodes(this EscapedString text)
        {
            return GetNodes(text.SelectMarkers().GetEnumerator(), text, 0, text.Length);
        }

        private static IEnumerable<INode> GetNodes(IEnumerator<MarkerPosition> markers, string text, int start,
            int upperBound)
        {
            var current = start;

            while (markers.MoveNext())
            {
                var marker = markers.Current;
                if (marker.Start >= upperBound)
                    break;
                if (current < marker.Start)
                    yield return new TextNode(text.Substring(current, marker.Start - current));

                var node = marker.Marker.CanContainNested
                    ? marker.Marker.CreateNode(GetNodes(markers, text, marker.Start + marker.Marker.AddOnStart,
                        marker.UpperBound - marker.Marker.AddOnEnd))
                    : marker.Marker.CreateNode(text, marker.Start, marker.UpperBound);
                yield return node;
                current = marker.UpperBound;
            }
            if (current < upperBound)
                yield return new TextNode(text.Substring(current, upperBound - current));
        }
    }
}