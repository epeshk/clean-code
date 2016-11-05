using System.IO;

namespace Markdown.TextRender
{
    public class RenderVisitor
    {
        protected TextWriter Writer;

        protected RenderVisitor()
        {
            
        }

        public RenderVisitor(TextWriter writer)
        {
            Writer = writer;
        }

        public virtual void StartItalic()
        {
            
        }
        public virtual void EndItalic()
        {
            
        }

        public virtual void StartBold()
        {
            
        }
        public virtual void EndBold()
        {
            
        }

        public virtual void StartParagrapn()
        {
            
        }
        public virtual void EndParagraph()
        {
            
        }

        public virtual void WriteText(string text)
        {
            
        }
    }
}