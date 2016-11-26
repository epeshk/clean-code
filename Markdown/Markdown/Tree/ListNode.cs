using System.Collections.Generic;
using Markdown.TextRender;

namespace Markdown.Tree
{
    internal class ListNode : StructureNode
    {
        public ListNode(IEnumerable<INode> nestedNodes) : base(nestedNodes)
        {
        }

        public override void Render(IRenderer renderer)
        {
            using (renderer.List())
            {
                base.Render(renderer);
            }
        }
    }
}