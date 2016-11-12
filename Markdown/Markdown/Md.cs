using System;
using System.IO;
using Markdown.TextParser;
using Markdown.TextRender;
using Markdown.Utilities;

namespace Markdown
{
    public static class Md
    {
        public static string Render(string markdown, RenderTarget renderTarget = RenderTarget.Html)
        {
            var parser = new MarkdownParser();
            var escapedString = new EscapedString(markdown);
            var markers = parser.GetRenderMarkers(escapedString);
            var writer = new StringWriter();
            var renderer = GetRenderer(renderTarget, writer);
            renderer.WriteText(escapedString, markers);

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