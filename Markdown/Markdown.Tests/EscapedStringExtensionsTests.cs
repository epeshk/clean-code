using FluentAssertions;
using Markdown.TextParser;
using Markdown.TextParser.Markers;
using Markdown.Utilities;
using NUnit.Framework;

namespace Markdown.Tests
{
    [TestFixture]
    internal class EscapedStringExtensionsTests
    {
        [TestCase("a_ b", 1, "_", TestName = "_SpaceAhead")]
        [TestCase(@"a\_b", 1, "_", TestName = "_Escaped")] // _ has index 1 in EscapedString
        [TestCase(@"2_", 1, "_", TestName = "_DigitBehind")]
        [TestCase(@"_2", 0, "_", TestName = "_DigitAhead")]
        [TestCase("a__ b", 1, "__", TestName = "__SpaceAhead")]
        [TestCase(@"a\__b", 1, "__", TestName = "__Escaped")]
        [TestCase(@"2__", 1, "__", TestName = "__DigitBehind")]
        [TestCase(@"__2", 0, "__", TestName = "__DigitAhead")]
        [TestCase("a___ b", 1, "___", TestName = "___SpaceAhead")]
        [TestCase(@"a\___b", 1, "___", TestName = "___Escaped")]
        [TestCase(@"2___", 1, "___", TestName = "___DigitBehind")]
        [TestCase(@"___2", 0, "___", TestName = "___DigitAhead")]
        public void MatchStart_should_not_match_incorrect_markers(string str, int position, string marker)
        {
            var escaped = new EscapedString(str);
            escaped.MatchStart(position, marker).Should().BeFalse();
        }

        [TestCase("a_b", 1, "_")]
        [TestCase("a _b", 2, "_")]
        [TestCase(@"_b", 0, "_")]
        [TestCase(@"\__b", 1, "_")]
        [TestCase("a__b", 1, "__")]
        [TestCase("a __b", 2, "__")]
        [TestCase(@"__b", 0, "__")]
        [TestCase(@"\___b", 1, "__")]
        [TestCase("a___b", 1, "___")]
        [TestCase("a ___b", 2, "___")]
        [TestCase(@"___b", 0, "___")]
        [TestCase(@"\____b", 1, "___")]
        public void MatchStart_should_match_correct_markers(string str, int position, string marker)
        {
            var escaped = new EscapedString(str);
            escaped.MatchStart(position, marker).Should().BeTrue();
        }

        [TestCase("a _b", 2, "_", TestName = "_SpaceBehind")]
        [TestCase(@"a\_b", 1, "_", TestName = "_Escaped")]
        [TestCase(@"2_", 1, "_", TestName = "_DigitBehind")]
        [TestCase(@"_2", 0, "_", TestName = "_DigitAhead")]
        [TestCase("a __b", 2, "__", TestName = "__SpaceBehind")]
        [TestCase(@"a\__b", 1, "__", TestName = "__Escaped")]
        [TestCase(@"2__", 1, "__", TestName = "__DigitBehind")]
        [TestCase(@"__2", 0, "__", TestName = "__DigitAhead")]
        [TestCase("a ___b", 2, "___", TestName = "___SpaceBehind")]
        [TestCase(@"a\___b", 1, "___", TestName = "___Escaped")]
        [TestCase(@"2___", 1, "___", TestName = "___DigitBehind")]
        [TestCase(@"___2", 0, "___", TestName = "___DigitAhead")]
        public void MatchEnd_should_not_match_incorrect_markers(string str, int position, string marker)
        {
            var escaped = new EscapedString(str);
            escaped.MatchEnd(position, marker).Should().BeFalse();
        }

        [TestCase("a_b", 1, "_")]
        [TestCase("a_ b", 1, "_")]
        [TestCase(@"_b", 0, "_")]
        [TestCase(@"\__b", 1, "_")]
        [TestCase("a__b", 1, "__")]
        [TestCase("a__ b", 1, "__")]
        [TestCase(@"__b", 0, "__")]
        [TestCase(@"\___b", 1, "__")]
        [TestCase("a___b", 1, "___")]
        [TestCase("a___ b", 1, "___")]
        [TestCase(@"___b", 0, "___")]
        [TestCase(@"\____b", 1, "___")]
        public void MatchEnd_should_match_correct_markers(string str, int position, string marker)
        {
            var escaped = new EscapedString(str);
            escaped.MatchEnd(position, marker).Should().BeTrue();
        }

        [TestCaseSource(nameof(GetMarkersPositionTestCases))]
        public void GetMarkersPosition_should_return_correct_markers_position(string paragraph, IMarker marker,
            MarkerPosition[] expected)
        {
            var escapedString = new EscapedString(paragraph);
            escapedString.GetMarkersPosition(marker).ShouldBeEquivalentTo(expected);
        }

        private static readonly TestCaseData[] GetMarkersPositionTestCases =
        {
            new TestCaseData("_a_", new ItalicMarker(), new[] {new MarkerPosition(0, 3, new ItalicMarker())}),
            new TestCaseData("__a__", new BoldMarker(), new[] {new MarkerPosition(0, 5, new BoldMarker())}),
            new TestCaseData("___a___", new ItalicBoldMarker(), new[] {new MarkerPosition(0, 7, new ItalicBoldMarker())}),
            new TestCaseData("ab_a_b_a_b", new ItalicMarker(),
                new[] {new MarkerPosition(2, 5, new ItalicMarker()), new MarkerPosition(6, 9, new ItalicMarker())})
        };
    }
}