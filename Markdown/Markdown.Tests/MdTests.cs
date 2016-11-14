using System.Collections.Generic;
using FluentAssertions;
using Markdown.TextParser;
using NUnit.Framework;

namespace Markdown.Tests
{
    [TestFixture]
    internal class MdTests
    {
        [TestCase("plain", Result = "plain", TestName = "Plain text")]
        [TestCase("_italic_", Result = "<em>italic</em>", TestName = "Italic")]
        [TestCase("__bold__", Result = "<strong>bold</strong>", TestName = "Bold")]
        [TestCase("__a_italic_a__", Result = "<strong>a<em>italic</em>a</strong>", TestName = "EmInStrong")]
        [TestCase("_it__al__ic_", Result = "<em>it__al__ic</em>", TestName = "ItalicWith__")]
        [TestCase("_abc", Result = "_abc", TestName = "Unclosed_")]
        [TestCase("__abc", Result = "__abc", TestName = "Unclosed__")]
        [TestCase("___abc___", Result = "<strong><em>abc</em></strong>", TestName = "Triple_")]
        [TestCase("___abc___", Result = "<strong><em>abc</em></strong>", TestName = "Triple_")]
        [TestCase("_ abc_", Result = "_ abc_", TestName = "SpaceAfterStart")]
        [TestCase("_abc _", Result = "_abc _", TestName = "SpaceBeforeEnd")]
        [TestCase("1_23_4", Result = "1_23_4", TestName = "_BetweenDigits")]
        [TestCase("a_23_b", Result = "a_23_b", TestName = "_BetweenTextAndDigits")]
        [TestCase("abc_def", Result = "abc_def", TestName = "Unclosed")]
        [TestCase("__ab _c", Result = "__ab _c", TestName = "NonPair")]
        [TestCase(@"\_a\_", Result = "_a_", TestName = "Escaped_")]
        [TestCase(@"\_\_a\_\_", Result = "__a__", TestName = "Escaped__")]
        [TestCase(@"\__a_\_", Result = "_<em>a</em>_", TestName = "Escape_NotGreedy")]
        public string Should_convert_to_Html_correctly(string markdown)
        {
            return Md.Render(markdown);
        }
    }

    [TestFixture]
    public class MarkdownEnumerableExtensionsTest
    {
        [Test]
        public void Should_remove_intersected_markers()
        {
            IEnumerable<MarkerPosition> enumerable = new[]
            {
                new MarkerPosition(0, 5, ""),
                new MarkerPosition(3, 7, ""),
                new MarkerPosition(8, 10, ""),
                new MarkerPosition(9, 13, ""),
                new MarkerPosition(11, 13, "")
            };

            enumerable.RemoveIntersectsMarkers().ShouldBeEquivalentTo(new[]
            {
                new MarkerPosition(0, 5, ""),
                new MarkerPosition(8, 10, ""),
                new MarkerPosition(11, 13, "")
            });
        }

        [Test]
        public void Should_remove_nestedIn_markers()
        {
            IEnumerable<MarkerPosition> enumerable = new[]
            {
                new MarkerPosition(0, 10, "_"),
                new MarkerPosition(1, 2, "_"),
                new MarkerPosition(5, 6, "_"),
                new MarkerPosition(7, 8, "_"),
                new MarkerPosition(11, 20, "__"),
                new MarkerPosition(12, 15, "_"),
                new MarkerPosition(13, 14, ""),
                new MarkerPosition(17, 18, " ")
            };
            enumerable.RemoveMarkersNestedIn("_").ShouldBeEquivalentTo(new[]
            {
                new MarkerPosition(0, 10, "_"),
                new MarkerPosition(11, 20, "__"),
                new MarkerPosition(12, 15, "_"),
                new MarkerPosition(17, 18, " ")
            });
        }
    }
}