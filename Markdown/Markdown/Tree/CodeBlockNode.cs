using Markdown.TextRender;

namespace Markdown.Tree
{
    internal class CodeBlockNode : TextNode
    {
        public CodeBlockNode(string innerText) : base(innerText)
        {
        }

        public override void Render(IRenderer renderer)
        {
            using (renderer.Preformatted())
            using (renderer.Code())
            {
                base.Render(renderer);
            }
        }
    }
}