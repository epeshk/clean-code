using Markdown.TextRender;

namespace Markdown.Nodes
{
    public class TextNode : INode
    {
        public string Text { get; }

        public TextNode(string text)
        {
            Text = text;
        }

        public virtual void Render(RenderVisitor renderer)
        {
            renderer.RenderTextNode(this);
        }
    }
}