using Markdown.TextRender;

namespace Markdown.Nodes
{
    public class ItalicNode : TextNode
    {
        public ItalicNode(string text) : base(text)
        {
        }

        public override void Render(RenderVisitor renderer)
        {
            renderer.RenderItalicNode(this);
        }
    }
}