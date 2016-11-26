using Markdown.TextRender;

namespace Markdown.Tree
{
    internal class LinkNode : INode
    {
        private readonly INode titleNode;
        private readonly string url;

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