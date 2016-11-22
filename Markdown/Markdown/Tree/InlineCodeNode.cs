using Markdown.TextRender;

namespace Markdown.Tree
{
    internal class InlineCodeNode : TextNode
    {
        public InlineCodeNode(string innerText) : base(innerText)
        {
        }

        public override void Render(IRenderer renderer)
        {
            using (renderer.Code())
            {
                base.Render(renderer);
            }
        }
    }
}