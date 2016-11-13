using System;
using System.IO;
using Markdown.TextParser;
using Markdown.TextRender;
using Markdown.Utilities;

namespace Markdown
{
    // Nit: IMO, name sucks
    public static class Md
    {
        public static string Render(string markdown, RenderTarget renderTarget = RenderTarget.Html)
        {
            var parser = new MarkdownParser();
            var escapedString = new EscapedString(markdown);
            var tagDescriptions = parser.GetTagDescriptions(escapedString);
            var writer = new StringWriter();
            var renderer = GetRenderer(renderTarget, writer);
            renderer.WriteText(escapedString, tagDescriptions);

            return writer.ToString();
        }

        private static IRenderer GetRenderer(RenderTarget target, TextWriter writer)
        {
            switch (target)
            {
                case RenderTarget.Html:
                    return new HtmlRenderer(writer);
                default:
                    throw new NotSupportedException();
            }
        }
    }
}