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

    public class TextNode : INode
    {
        protected readonly string InnerText;

        public TextNode(string innerText)
        {
            InnerText = innerText;
        }

        public virtual void Render(IRenderer renderer)
        {
            renderer.WriteText(InnerText);
        }
    }

    public class ItalicNode : TextNode
    {
        public ItalicNode(string innerText) : base(innerText)
        {
        }

        public override void Render(IRenderer renderer)
        {
            using (renderer.Italic())
            {
                base.Render(renderer);
            }
        }
    }


}