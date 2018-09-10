using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.IO;
using CodeAnalizer.FileAnalizerModule.Classes;
using CodeAnalizer;
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

            testPath = "D:/Test.txt";
            //File.Create(testPath);
            StreamWriter sw = new StreamWriter(new FileStream(testPath, FileMode.Create));
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

            miner = new DataMiner(LanguageSelector.GetMethodTemlate(),testPath);
        }
        [TearDown]
        public void CleanFile()
        {
            File.Delete("D:/Test.txt");
        }
        [Test]
        public void CountCharsTest()
        {
            int expected = 171;
            int output = miner.GetCharactersCount();
            Assert.AreEqual(expected, output);
        }
        [Test]
        public void CountLinesTest()
        {
            int expected = 12;
            int output = miner.GetLinesCount();
            Assert.AreEqual(expected, output);
        }
        [Test]
        public void CountUsingsTest()
        {
            int expected = 2;
            int output = miner.GetUsingsCount();
            Assert.AreEqual(expected, output);
        }
        [Test]
        public void CountEmptyLinesTest()
        {
            int expected = 1;
            int output = miner.GetEmptyLines();
            Assert.AreEqual(expected, output);
        }
        [Test]
        public void CountMethodsTest()
        {
            int expected = 1;
            int output = miner.GetMethodsCount();
            Assert.AreEqual(expected, output);
        }
        [Test]
        public void CountCommentsTest()
        {
            int expected = 5;
            int output = miner.GetCommentLines();
            Assert.AreEqual(expected, output);
        }

    }
}
