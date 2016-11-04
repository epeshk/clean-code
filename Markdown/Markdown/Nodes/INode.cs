using Markdown.TextRender;

namespace Markdown.Nodes
{
    public interface INode
    {
        void Render(RenderVisitor renderer);
    }
}