using System.IO;
using System.Web.UI;
using Markdown.Nodes;

namespace Markdown.TextRender
{
    public class HtmlRenderVisitor : RenderVisitor
    {
        private readonly HtmlTextWriter htmlWriter;

        public HtmlRenderVisitor(TextWriter writer)
        {
            Writer = htmlWriter = new HtmlTextWriter(writer, new string(' ', 4));
        }

        public override void RenderParagraphNode(ParagraphNode node)
        {
            using (htmlWriter.WriteTag("p"))
                RenderStructureNode(node);
        }

        public override void RenderBoldNode(BoldNode node)
        {
            using (htmlWriter.WriteTag("strong"))
                RenderStructureNode(node);
        }

        public override void RenderItalicNode(ItalicNode node)
        {
            using (htmlWriter.WriteTag("em"))
                RenderTextNode(node);
        }
    }
}