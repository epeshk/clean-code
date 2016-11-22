﻿using System.Collections.Generic;
using Markdown.Tree;

namespace Markdown.TextParser
{
    internal interface IParagraphKind
    {
        bool IsMatch(string str);
        INode CreateNode(string str, IEnumerable<INode> nodes);
        string RemoveWrapperMarkers(string str);
    }
}