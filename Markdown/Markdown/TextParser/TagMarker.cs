using System;
using System.Collections.Generic;
using Markdown.Tree;
using Markdown.Utilities;

namespace Markdown.TextParser
{
    internal class TagMarker : IMarker
    {
        private readonly string endTag;

        private readonly string startTag;

        public TagMarker(string tag, bool canContainNested)
        {
            startTag = endTag = tag;
            CanContainNested = canContainNested;
        }

        public TagMarker(string startTag, string endTag, bool canContainNested)
        {
            this.startTag = startTag;
            this.endTag = endTag;
            CanContainNested = canContainNested;
        }

        public virtual bool CanBeJoined(IMarker other, bool otherInLeft)
        {
            return false;
        }

        public bool CanContainNested { get; }
        public int AddOnStart => startTag.Length;
        public int AddOnEnd => endTag.Length;

        public virtual INode CreateNode(IEnumerable<INode> nestedNodes)
        {
            throw new NotImplementedException();
        }

        public virtual INode CreateNode(string s, int start, int end)
        {
            throw new NotImplementedException();
        }

        public bool MatchStart(EscapedString s, int position)
        {
            return s.MatchStart(position, startTag);
        }

        public bool MatchEnd(EscapedString s, int position)
        {
            return s.MatchEnd(position, endTag);
        }
    }

    internal class LinkNameMarker : TagMarker
    {
        public LinkNameMarker()
            : base("[", "]", false)
        {
        }

        public override bool CanBeJoined(IMarker other, bool otherInLeft)
        {
            if (other.GetType() != typeof(LinkUrlMarker))
                return false;
            return !otherInLeft;
        }
    }

    internal class LinkUrlMarker : TagMarker
    {
        public LinkUrlMarker()
            : base("(", ")", false)
        {
        }
        public override bool CanBeJoined(IMarker other, bool otherInLeft)
        {
            if (other.GetType() != typeof(LinkNameMarker))
                return false;
            return otherInLeft;
        } 
    }
}