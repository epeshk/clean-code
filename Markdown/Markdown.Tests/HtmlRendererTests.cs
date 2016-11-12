using System.IO;
using System.Text;
using FluentAssertions;
using Markdown.TextRender;
using NUnit.Framework;

namespace Markdown.Tests
{
    [TestFixture]
    public class HtmlRendererTests
    {
        [SetUp]
        public void SetUp()
        {
            builder = new StringBuilder();

            renderer = new HtmlRenderer(new StringWriter(builder));
        }

        private HtmlRenderer renderer;
        private StringBuilder builder;

        [Test]
        public void WriteText_should_write_text_with_nearly_tags()
        {
            var text = "abc___d___e";
            renderer.WriteText(text, new[]
            {
                new TagDescription(Tag.Bold, 3, 8, 2, 2),
                new TagDescription(Tag.Italic, 5, 7, 1, 1)
            });
            builder.ToString().Should().Be("abc<strong><em>d</em></strong>e");
        }

        [Test]
        public void WriteText_should_write_text_with_tags()
        {
            var text = "abc_d_e";
            renderer.WriteText(text, new[]
            {
                new TagDescription(Tag.Italic, 3, 5, 1, 1)
            });
            builder.ToString().Should().Be("abc<em>d</em>e");
        }
    }
}