using System;
using System.Collections.Generic;
using System.Text;

namespace Markdown.Utilities
{
    public class EscapedString
    {
        private readonly HashSet<int> escapedChars;
        private readonly string str;

        public EscapedString(string str)
        {
            escapedChars = new HashSet<int>();
            var builder = new StringBuilder(str.Length);
            var position = 0;
            var skipped = 0;
            while (position < str.Length)
            {
                if (str[position] == '\\')
                {
                    ++position;
                    ++skipped;
                    if (position < str.Length)
                    {
                        builder.Append(str[position]);
                        escapedChars.Add(position - skipped);
                    }
                }
                else
                    builder.Append(str[position]);
                ++position;
            }
            this.str = builder.ToString();
        }

        public int Length => str.Length;
        public char this[int index] => str[index];

        public bool OnChar(int position, Func<char, bool> func, bool defaultValue)
        {
            if (position < 0 || position >= str.Length || IsEscaped(position))
                return defaultValue;
            return func(str[position]);
        }

        public bool IsEscaped(int position)
        {
            return escapedChars.Contains(position);
        }

        public static implicit operator string(EscapedString escapedString)
        {
            return escapedString.str;
        }
    }
}