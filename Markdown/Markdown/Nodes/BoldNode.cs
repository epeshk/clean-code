using System.Collections.Generic;
using Markdown.TextRender;

namespace Markdown.Nodes
{
    public class BoldNode : IStructureNode
    {
        public IEnumerable<INode> NestedNodes => nestedNodes;

        private readonly List<INode> nestedNodes;

        public BoldNode(List<INode> nestedNodes)
        {
            this.nestedNodes = nestedNodes;
        }

        public void Render(RenderVisitor renderer)
        {
            renderer.RenderBoldNode(this);
        }
    }
}