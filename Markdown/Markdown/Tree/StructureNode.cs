using System.Collections.Generic;
using System.Linq;
using Markdown.TextRender;

namespace Markdown.Tree
{
    public class StructureNode : INode
    {
        protected readonly List<INode> NestedNodes;

        public StructureNode(IEnumerable<INode> nestedNodes)
        {
            NestedNodes = nestedNodes.ToList();
        }

        public virtual void Render(IRenderer renderer)
        {
            foreach (var node in NestedNodes)
                node.Render(renderer);
        }
    }
}