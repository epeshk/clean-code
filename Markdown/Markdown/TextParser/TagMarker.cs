using System;
using System.Collections.Generic;
using Markdown.Tree;
using Markdown.Utilities;

namespace Markdown.TextParser
{
    internal class TagMarker : IMarker
    {
        public bool CanContainNested { get; }
        public int AddOnStart => tag.Length;
        public int AddOnEnd => tag.Length;

        public virtual INode CreateNode(IEnumerable<INode> nestedNodes)
        {
            throw new NotImplementedException();
        }

        public virtual INode CreateNode(string s, int start, int end)
        {
            throw new NotImplementedException();
        }

        private readonly string tag;

        public TagMarker(string tag, bool canContainNested)
        {
            this.tag = tag;
            CanContainNested = canContainNested;
        }

        public bool MatchStart(EscapedString s, int position)
        {
            return s.MatchStart(position, tag);
        }

        public bool MatchEnd(EscapedString s, int position)
        {
            return s.MatchEnd(position, tag);
        }
    }
}