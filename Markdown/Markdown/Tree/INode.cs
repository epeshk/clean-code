using Markdown.TextRender;

namespace Markdown.Tree
{
    public interface INode
    {
        void Render(IRenderer renderer);
    }
}