using Markdown.Utilities;

namespace Markdown.TextRender
{
    public static class RendererExtensions
    {
        public static Handle Bold(this IRenderer renderer)
        {
            return new Handle(renderer.StartBold, renderer.EndBold);
        }

        public static Handle Italic(this IRenderer renderer)
        {
            return new Handle(renderer.StartItalic, renderer.EndItalic);
        }

        public static Handle Paragraph(this IRenderer renderer)
        {
            return new Handle(renderer.StartParagraph, renderer.EndParagraph);
        }

        public static Handle Preformatted(this IRenderer renderer)
        {
            return new Handle(renderer.StartPreformatted, renderer.EndPreformatted);
        }

        public static Handle Code(this IRenderer renderer)
        {
            return new Handle(renderer.StartCode, renderer.EndCode);
        }

        public static Handle Link(this IRenderer renderer, string url)
        {
            return new Handle(() => renderer.StartLink(url), renderer.EndLink);
        }

        public static Handle List(this IRenderer renderer)
        {
            return new Handle(renderer.StartList, renderer.EndList);
        }

        public static Handle ListEntry(this IRenderer renderer)
        {
            return new Handle(renderer.StartListEntry, renderer.EndListEntry);
        }

        public static Handle Header(this IRenderer renderer, int level)
        {
            return new Handle(
                () => renderer.StartHeader(level),
                () => renderer.EndHeader(level));
        }
    }
}