using System;
using System.IO;
using Markdown.TextParser;
using Markdown.TextRender;
using Markdown.Tree;

namespace Markdown
{
    public static class Md
    {
        public static string RenderParagraph(string markdown, RenderTarget renderTarget = RenderTarget.Html, string className = null)
        {
            var parser = new MarkdownParser();
            var root = parser.ParseSingleParagraph(markdown);
            return WriteNode(root, renderTarget, className);
        }

        public static string RenderWrappedParagraph(string markdown, RenderTarget renderTarget = RenderTarget.Html, string className = null)
        {
            var parser = new MarkdownParser();
            var root = parser.ParseSingleParagraph(markdown, true);
            return WriteNode(root, renderTarget, className);
        }

        public static string RenderText(string markdown, RenderTarget renderTarget = RenderTarget.Html, string className = null)
        {
            var parser = new MarkdownParser();
            var root = parser.ParseText(markdown);
            return WriteNode(root, renderTarget, className);
        }

        private static string WriteNode(INode root, RenderTarget renderTarget, string className)
        {
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