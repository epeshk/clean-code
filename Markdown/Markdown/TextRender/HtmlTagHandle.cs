using System;
using System.Web.UI;

namespace Markdown.TextRender
{
    internal class HtmlTagHandle : IDisposable
    {
        private readonly HtmlTextWriter writer;
        private readonly string tagName;

        public HtmlTagHandle(HtmlTextWriter writer, string tagName)
        {
            this.writer = writer;
            this.tagName = tagName;

            writer.WriteFullBeginTag(tagName);
        }


        public void Dispose()
        {
            writer.WriteEndTag(tagName);
        }
    }
}