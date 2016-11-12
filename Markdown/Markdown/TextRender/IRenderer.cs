using System.Collections.Generic;

namespace Markdown.TextRender
{
    public interface IRenderer
    {
        void WriteText(string text, IEnumerable<TagDescription> tagDescriptions);
    }
}