using System.Collections.Generic;
using Markdown.TextRender;

namespace Markdown.Nodes
{
    public class ParagraphNode : IStructureNode
    {
        public IEnumerable<INode> NestedNodes => nestedNodes;
        private readonly List<INode> nestedNodes;

        public ParagraphNode(List<INode> nestedNodes)
        {
            this.nestedNodes = nestedNodes;
        }

        public void Render(RenderVisitor renderer)
        {
            renderer.RenderParagraphNode(this);
        }
    }
}