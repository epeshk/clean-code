using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Markdown.TextRender
{
    internal class HtmlRenderer : IRenderer
    {
        private static readonly Dictionary<Tag, string> Tags = new Dictionary<Tag, string>
        {
            {Tag.Bold, "strong"},
            {Tag.Italic, "em"}
        };

        private readonly Dictionary<Tag, Position> markersToClose;
        private readonly Stack<Tag> tagStack;
        private readonly TextWriter writer;
        private char[] chars;
        private int offset;

        public HtmlRenderer(TextWriter writer)
        {
            this.writer = writer;
            tagStack = new Stack<Tag>();
            markersToClose = new Dictionary<Tag, Position>();
        }

        public void WriteText(string str, IEnumerable<TagDescription> tagDescriptions)
        {
            var tagsPositions = GetTagPositions(tagDescriptions);

            chars = str.ToCharArray();
            foreach (var tagPosition in tagsPositions)
            {
                RenderTag(tagPosition);
                CloseTags();
            }
            if (chars.Length - offset > 0)
                writer.Write(chars, offset, chars.Length - offset);
        }

        private static IEnumerable<Position> GetTagPositions(IEnumerable<TagDescription> tagDescriptions)
        {
            return tagDescriptions
                .SelectMany(x => new[]
                {
                    new Position(x.Start, true, x),
                    new Position(x.End, false, x)
                })
                .OrderBy(x => x.Index);
        }

        private void CloseTags()
        {
            while (TryCloseLastTag())
            {
            }
        }

        private bool TryCloseLastTag()
        {
            Position tagToClosePosition;
            if (!tagStack.Any() || !markersToClose.TryGetValue(tagStack.Peek(), out tagToClosePosition))
                return false;
            RenderTag(tagToClosePosition);
            return true;
        }

        private void RenderTag(Position tagPosition)
        {
            if (!TryStartRender(tagPosition))
                return;

            var plainTextLength = tagPosition.Index - offset;
            if (plainTextLength > 0)
                writer.Write(chars, offset, plainTextLength);
            IncreaseOffset(tagPosition);
            WriteTag(tagPosition.TagDescription.Tag, tagPosition.IsStart);
        }

        private void IncreaseOffset(Position tagPosition)
        {
            offset = Math.Max(tagPosition.Index, offset);
            if (tagPosition.IsStart)
                offset += tagPosition.TagDescription.SkipOnStart;
            else
                offset += tagPosition.TagDescription.SkipOnEnd;
        }

        private bool TryStartRender(Position tagPosition)
        {
            var tag = tagPosition.TagDescription.Tag;
            if (tagPosition.IsStart)
                tagStack.Push(tag);
            else if (tagStack.Any() && tagStack.Peek() == tag)
                tagStack.Pop();
            else
            {
                markersToClose[tag] = tagPosition;
                return false;
            }
            return true;
        }

        private void WriteTag(Tag tag, bool startTag)
        {
            writer.Write("<{0}{1}>", startTag ? "" : "/", Tags[tag]);
        }
    }
}