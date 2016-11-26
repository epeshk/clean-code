using System.Collections.Generic;
using Markdown.TextRender;

namespace Markdown.Tree
{
    internal class ListEntryNode : StructureNode
    {
        public ListEntryNode(IEnumerable<INode> nestedNodes) : base(nestedNodes)
        {
        }

        public override void Render(IRenderer renderer)
        {
            using (renderer.ListEntry())
            {
                base.Render(renderer);
            }
        }
    }
}