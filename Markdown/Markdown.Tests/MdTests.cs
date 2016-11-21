﻿using NUnit.Framework;

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
        [TestCase("___abc___", Result = "<strong><em>abc</em></strong>", TestName = "Triple_OnStart")]
        [TestCase("aaa___abc___", Result = "aaa<strong><em>abc</em></strong>", TestName = "Triple_")]
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
}