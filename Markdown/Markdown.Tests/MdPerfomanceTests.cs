using System.Text;
using NUnit.Framework;

namespace Markdown.Tests
{
    [TestFixture]
    public class MdPerfomanceTests
    {
        private string stringToParse;

        [SetUp]
        public void SetUp()
        {
            var sb = new StringBuilder(200000);
            for (var i = 0; i < 10000; ++i)
                sb.Append("___a___b__a_ab_a__".PadRight(20, 'a'));

            stringToParse = sb.ToString();
        }

        [Test]
        [Timeout(1000)]
        public string Should_word_fast()
        {
            return Md.Render(stringToParse);
        }
    }
}