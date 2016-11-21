using System;
using System.IO;
using Markdown.TextParser;
using Markdown.TextRender;
using Markdown.Utilities;

namespace Markdown
{
    public static class Md
    {
        public static string Render(string markdown, RenderTarget renderTarget = RenderTarget.Html, string className = null)
        {
            var parser = new MarkdownParser();
            var escapedString = new EscapedString(markdown);
            var root = parser.GetRoot(escapedString);
            var writer = new StringWriter();
            var renderer = GetRenderer(renderTarget, writer, className);
            root.Render(renderer);

            return writer.ToString();
        }

        private static IRenderer GetRenderer(RenderTarget target, TextWriter writer, string className)
        {
            switch (target)
            {
                case RenderTarget.Html:
                    return new HtmlRenderer(writer, className);
                default:
                    throw new NotSupportedException();
            }
        }
    }
}