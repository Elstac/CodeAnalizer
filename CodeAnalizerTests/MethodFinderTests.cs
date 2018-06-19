using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CodeAnalizer;
namespace CodeAnalizerTests
{
    [TestFixture]
    class MethodFinderTests
    {
        private MethodsFinder finder;
        [SetUp]
        public void PrepareFinder()
        {
            List<string>[] tmp = new List<string>[3];
            tmp[0] = new List<string>
            {
                "public",
                "private",
                "protected"
            };

            tmp[1] = new List<string>
            {
                "void",
                "int"
            };

            tmp[2] = new List<string>
            {
                "("
            };

            finder = new MethodsFinder(tmp, 2);
        }
        [Test]
        public void RemoveNameTest()
        {
            string expected = "(int test1,int test2)";
            string input = "Gibber(int test1,int test2)";
            finder.RemoveMethodName(ref input);
            Assert.AreEqual(expected,input );
        }
        [Test]
        public void RemoveNamelessTest()
        {
            string expected = "(int test1,int test2)";
            string input = "(int test1,int test2)";
            finder.RemoveMethodName(ref input);
            Assert.AreEqual(expected, input);
        }
        [Test]
        public void FindGoodMethodTest()
        {
            bool expected = true;
            string input = "public void f()";
            bool output = finder.IsMethod(input, 0);
            Assert.AreEqual(expected,output );
        }
        [Test]
        public void FindBadMethodTest()
        {
            bool expected = false;
            string input = "public int gibber;";
            Assert.AreEqual(expected, finder.IsMethod(input, 0));
        }
    }
}
