using System.IO;
using System.Web.UI;

namespace Markdown.TextRender
{
    public class HtmlRenderVisitor : RenderVisitor
    {
        private readonly HtmlTextWriter htmlWriter;

        public HtmlRenderVisitor(TextWriter writer)
        {
            Writer = htmlWriter = new HtmlTextWriter(writer, new string(' ', 4));
        }

        public override void StartBold()
        {
            base.StartBold();
        }

        public override void EndBold()
        {
            base.StartBold();
        }

        public override void StartItalic()
        {
            base.StartItalic();
        }
        public override void EndItalic()
        {
            base.StartItalic();
        }

        public override void StartParagrapn()
        {
            base.StartParagrapn();
        }

        public override void EndParagraph()
        {
            base.EndParagraph();
        }

        public override void WriteText(string text)
        {
            base.WriteText(text);
        }
    }
}