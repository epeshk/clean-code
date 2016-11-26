﻿using System;
using System.IO;
using Markdown.TextParser;
using Markdown.TextRender;
using Markdown.Tree;

namespace Markdown
{
    public static class Md
    {
        public static string Render(string markdown, RenderTarget renderTarget = RenderTarget.Html,
            string className = null, string baseUrl = null)
        {
            var parser = new MarkdownParser();
            var root = parser.ParseText(markdown);
            return WriteNode(root, renderTarget, className, baseUrl);
        }

        internal static string RenderParagraph(string markdown, RenderTarget renderTarget = RenderTarget.Html,
            string className = null, string baseUrl = null)
        {
            var parser = new MarkdownParser();
            var root = parser.ParseSingleParagraph(markdown);
            return WriteNode(root, renderTarget, className, baseUrl);
        }

        internal static string RenderWrappedParagraph(string markdown, RenderTarget renderTarget = RenderTarget.Html,
            string className = null, string baseUrl = null)
        {
            var parser = new MarkdownParser();
            var root = parser.ParseSingleParagraph(markdown, true);
            return WriteNode(root, renderTarget, className, baseUrl);
        }

        private static string WriteNode(INode root, RenderTarget renderTarget, string className, string baseUrl)
        {
            var writer = new StringWriter();
            var renderer = GetRenderer(renderTarget, writer, className, baseUrl);
            root.Render(renderer);
            return writer.ToString();
        }

        private static IRenderer GetRenderer(RenderTarget target, TextWriter writer, string className, string baseUrl)
        {
            switch (target)
            {
                case RenderTarget.Html:
                    return new HtmlRenderer(writer, className, baseUrl);
                default:
                    throw new NotSupportedException();
            }
        }
    }
}