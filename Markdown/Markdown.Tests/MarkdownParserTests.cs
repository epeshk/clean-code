using FluentAssertions;
using Markdown.TextParser;
using Markdown.Utilities;
using NUnit.Framework;

namespace Markdown.Tests
{
    [TestFixture]
    internal class MarkdownParserTests
    {
        [SetUp]
        public void SetUp()
        {
            markdownParser = new MarkdownParser();
        }

        private static readonly TestCaseData[] GetMarkersPositionTestCases =
        {
            new TestCaseData("_a_", "_", new[] {new MarkerPosition(0, 2, "_")}),
            new TestCaseData("__a__", "__", new[] {new MarkerPosition(0, 3, "__")}),
            new TestCaseData("___a___", "___", new[] {new MarkerPosition(0, 4, "___")}),
            new TestCaseData("ab_a_b_a_b", "_", new[] {new MarkerPosition(2, 4, "_"), new MarkerPosition(6, 8, "_")})
        };

        private MarkdownParser markdownParser;

        [TestCaseSource(nameof(GetMarkersPositionTestCases))]
        public void GetMarkersPosition_should_return_correct_markers_position(string paragraph, string marker,
            MarkerPosition[] expected)
        {
            var escapedString = new EscapedString(paragraph);
            markdownParser.GetMarkersPosition(escapedString, marker).ShouldBeEquivalentTo(expected);
        }
    }
}