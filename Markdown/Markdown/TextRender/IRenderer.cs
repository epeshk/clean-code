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

        void StartPreformatted();
        void EndPreformatted();

        void StartCode();
        void EndCode();

        void StartHeader(int level);
        void EndHeader(int level);

        void StartLink(string url);
        void EndLink();
    }
}