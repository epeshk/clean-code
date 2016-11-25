using System.Collections.Generic;
using System.IO;

namespace Markdown.TextRender
{
    internal class HtmlRenderer : IRenderer
    {
        private static readonly Dictionary<Tag, string> Tags = new Dictionary<Tag, string>
        {
            {Tag.Bold, "strong"},
            {Tag.Italic, "em"},
            {Tag.Paragraph, "p"},
            {Tag.Preformatted, "pre"},
            {Tag.Code, "code"}
        };

        private readonly string className;

        private readonly TextWriter writer;

        public HtmlRenderer(TextWriter writer, string className = null)
        {
            this.writer = writer;
            this.className = className;
        }

        public void WriteText(string text)
        {
            writer.Write(text);
        }

        public void StartBold()
        {
            WriteStartTag(Tag.Bold);
        }

        public void EndBold()
        {
            WriteEndTag(Tag.Bold);
        }

        public void StartItalic()
        {
            WriteStartTag(Tag.Italic);
        }

        public void EndItalic()
        {
            WriteEndTag(Tag.Italic);
        }

        public void StartParagraph()
        {
            WriteStartTag(Tag.Paragraph);
        }

        public void EndParagraph()
        {
            WriteEndTag(Tag.Paragraph);
        }

        public void StartPreformatted()
        {
            WriteStartTag(Tag.Preformatted);
        }

        public void EndPreformatted()
        {
            WriteEndTag(Tag.Preformatted);
        }

        public void StartCode()
        {
            WriteStartTag(Tag.Code);
        }

        public void EndCode()
        {
            WriteEndTag(Tag.Code);
        }

        public void StartHeader(int level)
        {
            WriteStartTag("h" + level);
        }

        public void EndHeader(int level)
        {
            WriteEndTag("h" + level);
        }

        private void WriteStartTag(Tag tag)
        {
            WriteStartTag(Tags[tag]);
        }

        private void WriteStartTag(string tag)
        {
            if (className != null)
            {
                writer.Write("<{0} class=\"{1}\">", tag, className);
                return;
            }
            writer.Write("<{0}>", tag);
        }

        private void WriteEndTag(Tag tag)
        {
            WriteEndTag(Tags[tag]);
        }

        private void WriteEndTag(string tag)
        {
            writer.Write("</{0}>", tag);
        }
    }
}