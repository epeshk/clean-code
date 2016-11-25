using System.Linq;
using Markdown.Tree;
using Markdown.Utilities;

namespace Markdown.TextParser.Paragraphs
{
    internal class Header : IParagraphKind
    {
        public INode ParseOrNull(string str, bool wrap)
        {
            if (string.IsNullOrEmpty(str))
                return null;
            var position = 0;
            while (position < str.Length && str[position] == '#')
                ++position;
            if (!(position > 0 && position < str.Length && str[position - 1] == '#' && str[position] == ' '))
                return null;
            return CreateNode(str);
        }

        private INode CreateNode(string str)
        {
            var escaped = new EscapedString(RemoveWrapperMarkers(str));
            var nodes = escaped.GetNodes();
            return new HeaderNode(nodes, Level(str));
        }

        private string RemoveWrapperMarkers(string str)
        {
            return str.Trim('#', ' ');
        }

        private int Level(string str)
        {
            return str.TakeWhile(c => c == '#').Count();
        }
    }
}