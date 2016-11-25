using System.Text;
using NUnit.Framework;

namespace Markdown.Tests
{
    [Explicit]
    [TestFixture]
    public class MdPerfomanceTests
    {
        private string stringToParse1;
        private string stringToParse2;

        [TestFixtureSetUp]
        public void SetUp()
        {
            var sb = new StringBuilder(200000);
            for (var i = 0; i < 10000; ++i)
                sb.Append("___a___b__a_ab_a__".PadRight(20, 'a'));

            stringToParse1 = sb.ToString();

            sb.Clear();
            sb.Append("__a");
            for (var i = 1; i < 10000; ++i)
                sb.Append("___a_a_a_a___a___".PadRight(20, 'a'));

            stringToParse2 = sb.ToString();
        }

        [Test]
        [Timeout(1000)]
        public string Should_not_be_very_slow1()
        {
            return Md.RenderParagraph(stringToParse1);
        }

        [Test]
        [Timeout(1000)]
        public string Should_not_be_very_slow2()
        {
            return Md.RenderParagraph(stringToParse2);
        }
    }
}