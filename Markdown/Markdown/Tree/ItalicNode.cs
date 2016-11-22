using Markdown.TextRender;

namespace Markdown.Tree
{
    public class ItalicNode : TextNode
    {
        public ItalicNode(string innerText) : base(innerText)
        {
        }

        public override void Render(IRenderer renderer)
        {
            using (renderer.Italic())
            {
                base.Render(renderer);
            }
        }
    }
}