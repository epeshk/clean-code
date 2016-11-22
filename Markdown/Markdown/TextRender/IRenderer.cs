using Markdown.Utilities;

namespace Markdown.TextRender
{
    public interface IRenderer
    {
        void WriteText(string text);

        void StartBold();
        void EndBold();

        void StartItalic();
        void EndItalic();

        void StartParagraph();
        void EndParagraph();

        void StartHeader(int level);
        void EndHeader(int level);
    }

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

        public static Handle Header(this IRenderer renderer, int level)
        {
            return new Handle(
                () => renderer.StartHeader(level),
                () => renderer.EndHeader(level));
        }
    }
}