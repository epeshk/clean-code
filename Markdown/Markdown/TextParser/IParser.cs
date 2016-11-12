﻿using System.Collections.Generic;
using Markdown.TextRender;
using Markdown.Utilities;

namespace Markdown.TextParser
{
    public interface IParser
    {
        IEnumerable<TagDescription> GetTagDescriptions(EscapedString str);
    }
}