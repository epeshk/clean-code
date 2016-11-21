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
            new TestCaseData("_a_", new ItalicMarker(), new[] {new MarkerPosition(0, 3, new ItalicMarker())}),
            new TestCaseData("__a__", new BoldMarker(), new[] {new MarkerPosition(0, 5, new BoldMarker())}),
            new TestCaseData("___a___", new ItalicBoldMarker(), new[] {new MarkerPosition(0, 7, new ItalicBoldMarker())}),
            new TestCaseData("ab_a_b_a_b", new ItalicMarker(), new[] {new MarkerPosition(2, 5, new ItalicMarker()), new MarkerPosition(6, 9, new ItalicMarker())})
        };

        private MarkdownParser markdownParser;

        [TestCaseSource(nameof(GetMarkersPositionTestCases))]
        public void GetMarkersPosition_should_return_correct_markers_position(string paragraph, IMarker marker,
            MarkerPosition[] expected)
        {
            var escapedString = new EscapedString(paragraph);
            markdownParser.GetMarkersPosition(escapedString, marker).ShouldBeEquivalentTo(expected);
        }
    }
}