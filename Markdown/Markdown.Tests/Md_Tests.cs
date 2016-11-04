using NUnit.Framework;

namespace Markdown.Tests
{
    [TestFixture]
    internal class Md_Tests
    {
        [TestCase("_italic_", Result = "<em>italic</em>", TestName = "Italic")]
        [TestCase("__bold__", Result = "<strong>bold</strong>", TestName = "Italic")]
        [TestCase("__a_italic_a__", Result = "<strong>a<em>italic</em>a</strong>", TestName = "EmInStrong")]
        [TestCase("_it__al__ic_", Result = "<em>it__al__ic</em>", TestName = "ItalicWith__")]
        [TestCase("_abc", Result = "_abc", TestName = "Unclosed_")]
        [TestCase("__abc", Result = "__abc", TestName = "Unclosed__")]
        [TestCase(@"\_a\_", Result = "_a_", TestName = "Escaped_")]
        [TestCase(@"\_\_a\_\_", Result = "_a_", TestName = "Escaped__")]
        [TestCase(@"\__a_\_", Result = "_<em>a_</em>_", TestName = "Escape_NotGreedy")]
        [TestCase(@"\__a_\_", Result = "_<em>a_</em>_", TestName = "Save_If")]
        public string Should_convert_to_Html_correct(string markdown)
        {
            return Md.Render(markdown);
        }
        
    }
}