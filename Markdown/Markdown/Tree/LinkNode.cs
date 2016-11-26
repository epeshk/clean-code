using Markdown.TextRender;

namespace Markdown.Tree
{
    internal class LinkNode : INode
    {
        private readonly string url;
        private readonly INode titleNode;

        public LinkNode(string url, INode titleNode = null)
        {
            this.url = url;
            this.titleNode = titleNode;
        }

        public void Render(IRenderer renderer)
        {
            using (renderer.Link(url))
            {
                titleNode.Render(renderer);
            }
        }
    }
}