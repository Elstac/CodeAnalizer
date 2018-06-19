using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CodeAnalizer;
using System.IO;
namespace CodeAnalizerTests
{
    [TestFixture]
    public class DataMinerTest
    {
        private DataMiner miner;
        private string testPath;
        [SetUp]
        public void PrepareTestFile()
        {
            LanguageSelector.Language = Language.Csharp;
            miner = new DataMiner(LanguageSelector.GetMethodTemlate(),LanguageSelector.GetNamePosition());
            
            testPath = "D:/Test.txt";
            //File.Create(testPath);
            StreamWriter sw = new StreamWriter(new FileStream(testPath,FileMode.Create));
            sw.WriteLine("using Test;");
            sw.WriteLine("using Test2;");
            sw.WriteLine("///<summary>Test file method<summary>");
            sw.WriteLine("public void Test(int x)");
            sw.WriteLine("{");
            sw.WriteLine("   int test;");
            sw.WriteLine("   //Add test int variable");
            sw.WriteLine("}");
            sw.WriteLine("    ");
            sw.WriteLine("/*To Add:");
            sw.WriteLine("-Test float variable");
            sw.WriteLine("*/");
            sw.WriteLine("   private double BigTest;");

            sw.Close();
        }
        [Test]
        public void CountCharsTest()
        {
            int expected = 171;
            int output = miner.CountCharacters(testPath);
            Assert.AreEqual(expected, output);
        }
        [Test]
        public void CountLinesTest()
        {
            int expected = 12;
            int output = miner.CountLines(testPath);
            Assert.AreEqual(expected, output);
        }
        [Test]
        public void CountUsingsTest()
        {
            int expected = 2;
            int output = miner.CountUsings(testPath);
            Assert.AreEqual(expected, output);
        }
        [Test]
        public void CountEmptyLinesTest()
        {
            int expected = 1;
            int output = miner.CountEmpty(testPath);
            Assert.AreEqual(expected, output);
        }
        [Test]
        public void CountMethodsTest()
        {
            int expected = 1;
            int output = miner.CountMethods(testPath);
            Assert.AreEqual(expected, output);
        }
        [Test]
        public void CountCommentsTest()
        {
            int expected = 5;
            int output = miner.CountComment(testPath);
            Assert.AreEqual(expected, output);
        }
    }
}
