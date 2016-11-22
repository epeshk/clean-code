using System.Collections.Generic;
using Markdown.TextRender;

namespace Markdown.Tree
{
    public class ParagraphNode : StructureNode
    {
        public ParagraphNode(IEnumerable<INode> nestedNodes) : base(nestedNodes)
        {
        }

        public override void Render(IRenderer renderer)
        {
            using (renderer.Paragraph())
            {
                base.Render(renderer);
            }
        }
    }
}