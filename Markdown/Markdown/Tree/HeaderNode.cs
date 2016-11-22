using System.Collections.Generic;
using Markdown.TextRender;

namespace Markdown.Tree
{
    public class HeaderNode : StructureNode
    {
        private readonly int level;

        public HeaderNode(IEnumerable<INode> nestedNodes, int level) : base(nestedNodes)
        {
            this.level = level;
        }

        public override void Render(IRenderer renderer)
        {
            using (renderer.Header(level))
            {
                base.Render(renderer);
            }
        }
    }
}