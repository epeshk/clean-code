using Markdown.TextRender;
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
        [TestCase(@"", Result = "", TestName = "Empty")]
        [TestCase(@"a`b`a", Result = "a<code>b</code>a", TestName = "Inline code")]
        [TestCase(@"[title](link)", Result = "<a href=\"link\">title</a>", TestName = "OnlyLink")]
        [TestCase(@"a[title](link)a", Result = "a<a href=\"link\">title</a>a", TestName = "Link")]
        [TestCase(@"a[title]a(link)", Result = @"a[title]a(link)")]
        [TestCase(@"[_title_](link)", Result = "<a href=\"link\"><em>title</em></a>", TestName = "ItalicLink")]
        [TestCase("1.  abcd\n2.  ef", Result = "<ol><li>abcd</li><li>ef</li></ol>", TestName = "List")]
        [TestCase("5.  abcd\n2.  ef", Result = "<ol><li>abcd</li><li>ef</li></ol>",
            TestName = "List with unordered indexed items")]
        public string Should_convert_to_Html_correctly(string markdown)
        {
            return Md.Render(markdown, RenderTarget.Html, null, null, false);
        }

        [TestCase(@"[title](/link)", Result = "<a href=\"base:/link\">title</a>", TestName = "OnlyLink")]
        [TestCase(@"a[title](/link)a", Result = "a<a href=\"base:/link\">title</a>a", TestName = "Link")]
        public string Should_respect_base_url(string markdown)
        {
            return Md.Render(markdown, RenderTarget.Html, null, "base:", false);
        }

        [TestCase(@"_a_", Result = "<em class=\"x\">a</em>", TestName = "To one tag")]
        [TestCase(@"__a_b_c__", Result = "<strong class=\"x\">a<em class=\"x\">b</em>c</strong>",
            TestName = "To many tags")]
        public string Should_add_className_if_specified(string markdown)
        {
            return Md.Render(markdown, RenderTarget.Html, "x", null, false);
        }

        [TestCase("a\r\n\r\nb", Result = "<p>a</p><p>b</p>", TestName = "CRLF")]
        [TestCase("a\n\nb", Result = "<p>a</p><p>b</p>", TestName = "LF")]
        [TestCase("a\n\r\nb", Result = "<p>a</p><p>b</p>", TestName = "MIXED")]
        public string RenderText_Should_split_result_to_paragraphs_With_line_endings(string markdown)
        {
            return Md.Render(markdown, RenderTarget.Html);
        }

        [TestCase("###A", Result = "<p>###A</p>", TestName = "Not header")]
        [TestCase("# a", Result = "<h1>a</h1>", TestName = "h1")]
        [TestCase("### a ##", Result = "<h3>a</h3>", TestName = "double wrap")]
        public string RenderParagraph_Should_render_headers(string markdown)
        {
            return Md.Render(markdown, RenderTarget.Html);
        }

        [TestCase("    a\n    b", Result = "<pre><code>a\nb</code></pre>", TestName = "Space indented")]
        [TestCase("\ta\n\tb", Result = "<pre><code>a\nb</code></pre>", TestName = "Tab indented")]
        [TestCase("\ta\n\t\tb", Result = "<pre><code>a\n\tb</code></pre>", TestName = "Save tabs")]
        [TestCase("\ta\n\t    b", Result = "<pre><code>a\n    b</code></pre>", TestName = "Save spaces")]
        public string RenderParagraph_Should_render_code_blocks(string markdown)
        {
            return Md.Render(markdown, RenderTarget.Html);
        }

        [TestCase("    code [title](link) ccc", Result = "<pre><code>code [title](link) ccc</code></pre>",
            TestName = "Link")]
        [TestCase("    co __d__ e", Result = "<pre><code>co __d__ e</code></pre>", TestName = "Bold")]
        [TestCase("    co _d_ e", Result = "<pre><code>co _d_ e</code></pre>", TestName = "Italic")]
        public string CodeBlock_should_not_contain(string markdown)
        {
            return Md.Render(markdown, RenderTarget.Html);
        }

        [TestCase("### a [link](link) b", Result = "<h3>a <a href=\"link\">link</a> b</h3>", TestName = "Link")]
        [TestCase("### a __b__ c", Result = "<h3>a <strong>b</strong> c</h3>", TestName = "Bold")]
        [TestCase("### a _b_ c", Result = "<h3>a <em>b</em> c</h3>", TestName = "Italic")]
        public string Header_can_contain(string markdown)
        {
            return Md.Render(markdown, RenderTarget.Html);
        }

        [TestCase("1.  a [link](link) b", Result = "<ol><li>a <a href=\"link\">link</a> b</li></ol>", TestName = "Link")
        ]
        [TestCase("2.  a __b__ c", Result = "<ol><li>a <strong>b</strong> c</li></ol>", TestName = "Bold")]
        [TestCase("3.  a _b_ c", Result = "<ol><li>a <em>b</em> c</li></ol>", TestName = "Italic")]
        public string List_can_contain(string markdown)
        {
            return Md.Render(markdown, RenderTarget.Html);
        }
    }
}