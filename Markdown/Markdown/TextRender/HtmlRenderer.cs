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
            {Tag.Paragraph, "p"}
        };

        private readonly TextWriter writer;

        public HtmlRenderer(TextWriter writer)
        {
            this.writer = writer;
        }

        private void WriteStartTag(Tag tag)
        {
            writer.Write("<{0}>", Tags[tag]);
        }

        private void WriteEndTag(Tag tag)
        {
            writer.Write("</{0}>", Tags[tag]);
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
    }
}