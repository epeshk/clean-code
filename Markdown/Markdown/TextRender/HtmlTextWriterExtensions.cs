using System.Web.UI;

namespace Markdown.TextRender
{
    internal static class HtmlTextWriterExtensions
    {
        public static HtmlTagHandle WriteTag(this HtmlTextWriter writer, string tagName)
        {
            return new HtmlTagHandle(writer, tagName);
        }
    }
}