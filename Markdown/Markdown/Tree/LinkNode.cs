using Markdown.TextRender;

namespace Markdown.Tree
{
    internal class LinkNode : TextNode
    {
        private readonly string url;

        public LinkNode(string url, string name = null) : base(name ?? url)
        {
            this.url = url;
        }

        public override void Render(IRenderer renderer)
        {
            using (renderer.Link(url))
            {
                base.Render(renderer);
            }
        }
    }
}