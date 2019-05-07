using NUnit.Framework;
using SimpleGames;

namespace Tests
{
    [TestFixture]
    public class XMlTests
    {
        XML xmlclass = new XML();
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(3)]
        [TestCase(0)]
        [TestCase(2)]
        public void XMLRead (int i)
        {
            string word = xmlclass.ReadWord(i);
            Assert.IsNotNull(word);
        }
    }
}