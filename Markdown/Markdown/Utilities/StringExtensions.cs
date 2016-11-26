namespace Markdown.Utilities
{
    internal static class StringExtensions
    {
        public static bool IsNumber(this string str)
        {
            long num;
            return long.TryParse(str, out num);
        }
    }
}