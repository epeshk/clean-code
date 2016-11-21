using System.Collections.Generic;
using FluentAssertions;
using Markdown.TextParser;
using NUnit.Framework;

namespace Markdown.Tests
{
    [TestFixture]
    public class MarkdownEnumerableExtensionsTests
    {
        [Test]
        public void Should_remove_intersected_markers()
        {
            var marker = new TagMarker("", true);
            IEnumerable<MarkerPosition> enumerable = new[]
            {
                new MarkerPosition(0, 5, marker),
                new MarkerPosition(3, 7, marker),
                new MarkerPosition(8, 10, marker),
                new MarkerPosition(9, 13, marker),
                new MarkerPosition(11, 13, marker)
            };

            enumerable.RemoveIntersectsMarkers().ShouldBeEquivalentTo(new[]
            {
                new MarkerPosition(0, 5, marker),
                new MarkerPosition(8, 10, marker),
                new MarkerPosition(11, 13, marker)
            });
        }

        [Test]
        public void Should_remove_nestedIn_markers()
        {
            var empty = new TagMarker("", true);
            var italic = new ItalicMarker();
            var bold = new BoldMarker();
            IEnumerable<MarkerPosition> enumerable = new[]
            {
                new MarkerPosition(0, 10, italic),
                new MarkerPosition(1, 2, italic),
                new MarkerPosition(5, 6, italic),
                new MarkerPosition(7, 8, italic),
                new MarkerPosition(11, 20, bold),
                new MarkerPosition(12, 15, italic),
                new MarkerPosition(13, 14, empty),
                new MarkerPosition(17, 18, italic)
            };

            enumerable.RemoveRedundantNestedMarkers().ShouldBeEquivalentTo(new[]
            {
                new MarkerPosition(0, 10, italic),
                new MarkerPosition(11, 20, bold),
                new MarkerPosition(12, 15, italic),
                new MarkerPosition(17, 18, italic)
            });
        }
    }
}