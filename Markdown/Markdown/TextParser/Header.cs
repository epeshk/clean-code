using System.Collections.Generic;
using System.Linq;
using Markdown.Tree;

namespace Markdown.TextParser
{
    internal class Header : IParagraphKind
    {
        public bool IsMatch(string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            var position = 0;
            while (position < str.Length && str[position] == '#')
                ++position;
            return position > 0 && position < str.Length && str[position - 1] == '#' && str[position] == ' ';
        }

        public StructureNode CreateNode(string str, IEnumerable<INode> nodes)
        {
            return new HeaderNode(nodes, Level(str));
        }

        public string RemoveWrapperMarkers(string str)
        {
            return str.Trim('#', ' ');
        }
        private int Level(string str)
        {
            return str.TakeWhile(c => c == '#').Count();
        }
    }
}