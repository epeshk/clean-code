using FluentAssertions;
using Markdown.Utilities;
using NUnit.Framework;

namespace Markdown.Tests
{
    [TestFixture]
    public class EscapedStringTests
    {
        [TestCase(@"ab\cd\ef\gh", Result = "abcdefgh")]
        [TestCase(@"ab\\cd", Result = @"ab\cd")]
        [TestCase(@"ab\\\cd", Result = @"ab\cd")]
        [TestCase(@"ab\\\\cd", Result = @"ab\\cd")]
        public string Should_not_contains_escaping_slashes(string str)
        {
            return new EscapedString(str);
        }

        [Test]
        public void IsEscaped_should_be_False_on_non_escaped_chars_positions()
        {
            var str = new EscapedString(@"a\bc\d");
            str.IsEscaped(0).Should().BeFalse();
            str.IsEscaped(2).Should().BeFalse();
        }

        [Test]
        public void IsEscaped_should_be_True_on_escaped_chars_positions()
        {
            var str = new EscapedString(@"a\bc\d");
            str.IsEscaped(1).Should().BeTrue();
            str.IsEscaped(3).Should().BeTrue();
        }
    }
}