using Markdown.TextRender;

namespace Markdown.Tree
{
    public class TextNode : INode
    {
        protected readonly string InnerText;

        public TextNode(string innerText)
        {
            InnerText = innerText;
        }

        public virtual void Render(IRenderer renderer)
        {
            renderer.WriteText(InnerText);
        }
    }
}