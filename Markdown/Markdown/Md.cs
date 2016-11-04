using System;
using System.IO;
using Markdown.TextParser;
using Markdown.TextRender;

namespace Markdown
{
    public static class Md
    {
        public static string Render(string markdown, RenderTarget renderTarget = RenderTarget.Html)
        {
            var parser = new MarkdownParser();
            var nodes = parser.ParseToNodes(markdown);
            var writer = new StringWriter();
            var renderer = GetRenderer(renderTarget, writer);
            foreach (var node in nodes)
                node.Render(renderer);

            return writer.ToString();
        }

        private static RenderVisitor GetRenderer(RenderTarget target, TextWriter writer)
        {
            switch (target)
            {
                case RenderTarget.Html:
                    return new RenderVisitor(writer);
            }

            throw new NotSupportedException();
        }
    }
}