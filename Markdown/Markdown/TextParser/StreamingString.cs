namespace Markdown.TextParser
{
    public class StreamingString
    {
        private readonly string str;
        public int Position { get; set; }

        public StreamingString(string str)
        {
            this.str = str;
        }

        public static implicit operator StreamingString(string str) => new StreamingString(str);
        public static implicit operator string(StreamingString str) => str.str;
    }
}