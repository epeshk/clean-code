using System.Collections.Generic;
using Markdown.TextRender;

namespace Markdown.Tree
{
    public class BoldNode : StructureNode
    {
        public BoldNode(IEnumerable<INode> nestedNodes) : base(nestedNodes)
        {
        }

        public override void Render(IRenderer renderer)
        {
            using (renderer.Bold())
            {
                base.Render(renderer);
            }
        }
    }
}