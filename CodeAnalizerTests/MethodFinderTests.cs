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
            List<List<string>> tmp = new List<List<string>>
            {
                new List<string>
                {
                    "public",
                    "private",
                    "protected"
                },
                new List<string>
                {
                    "static",
                    "abstract",
                    "override",
                    "seald"
                },
                new List<string>
                {
                    "void",
                    "int",
                    "float",
                    "double"
                },
                new List<string>
                {
                    "#",
                    "+",
                    "#",
                    "("
                },
            };
            finder = new MethodsFinder(tmp.ToArray());
        }
        
        [Test]
        public void RemoveNameTest()
        {
            string expected = "(int test1,int test2)";
            string input = "Gibber(int test1,int test2)";
            finder.RemoveMethodName(ref input,"(");
            Assert.AreEqual(expected,input );
        }
        [Test]
        public void RemoveNamelessTest()
        {
            string expected = "(int test1,int test2)";
            string input = "(int test1,int test2)";
            finder.RemoveMethodName(ref input,"(");
            Assert.AreEqual(expected, input);
        }
        [Test]
        public void FindSimpleMethodTest()
        {
            bool expected = true;
            string input = "public void f();";
            bool output = finder.IsMethod(input);
            Assert.AreEqual(expected,output );
        }
        [Test]
        public void FindComplexMethodTest()
        {
            bool expected = true;
            string input = "public abstract override void f(int gej);";
            bool output = finder.IsMethod(input);
            Assert.AreEqual(expected, output);
        }
        [Test]
        public void FindBadMethodTest()
        {
            bool expected = false;
            string input = "public int gibber;";
            Assert.AreEqual(expected, finder.IsMethod(input));
        }

    }
}
