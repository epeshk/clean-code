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
            {Tag.Code, "code"},
            {Tag.Anchor, "a"}
        };

        private readonly string baseUrl;

        private readonly string className;

        private readonly TextWriter writer;

        public HtmlRenderer(TextWriter writer, string className = null, string baseUrl = null)
        {
            this.writer = writer;
            this.className = className;
            this.baseUrl = baseUrl;
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

        public void StartList()
        {
            WriteStartTag("ol");
        }

        public void EndList()
        {
            WriteEndTag("ol");
        }

        public void StartListEntry()
        {
            WriteStartTag("li");
        }

        public void EndListEntry()
        {
            WriteEndTag("li");
        }

        public void StartHeader(int level)
        {
            WriteStartTag($"h{level}");
        }

        public void EndHeader(int level)
        {
            WriteEndTag($"h{level}");
        }

        public void StartLink(string url)
        {
            WriteLinkStartTag(url);
        }

        public void EndLink()
        {
            WriteEndTag(Tag.Anchor);
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

        private void WriteLinkStartTag(string url)
        {
            var absoluteUrl = baseUrl != null && url.StartsWith("/") ? baseUrl + url : url;
            if (className != null)
            {
                writer.Write("<a href=\"{0}\" class=\"{1}\">", absoluteUrl, className);
                return;
            }
            writer.Write("<a href=\"{0}\">", absoluteUrl);
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