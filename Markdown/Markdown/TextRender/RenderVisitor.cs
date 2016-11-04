using System.IO;
using Markdown.Nodes;

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

        public virtual void RenderItalicNode(ItalicNode node)
        {
            RenderTextNode(node);
        }

        public virtual void RenderBoldNode(BoldNode node)
        {
            RenderStructureNode(node);
        }

        public virtual void RenderParagraphNode(ParagraphNode node)
        {
            RenderStructureNode(node);
        }

        public virtual void RenderTextNode(TextNode node)
        {
            Writer.Write(node.Text);
        }

        protected virtual void RenderStructureNode(IStructureNode node)
        {
            foreach (var nestedNode in node.NestedNodes)
                nestedNode.Render(this);
            
        }
    }
}