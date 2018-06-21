using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.IO;
using CodeAnalizer;
namespace CodeAnalizerTests
{
    [TestFixture]
    class ListerTests
    {
        private Lister lister = new Lister(new string[] { ".cs", "xaml.cs" }, new string[] { "D:\\TestFolder\\Layer1\\BadFolder" });
        private string[] paths = new string[] {
                "D:\\TestFolder\\Layer1",
                "D:\\TestFolder\\Layer1\\Layer2.1",
                "D:\\TestFolder\\Layer1\\BadFolder"
            };
        [OneTimeSetUp]
        public void CreateDirectories()
        {
            foreach (var path in paths)
            {
                Directory.CreateDirectory(path);
                CreateFiles(path);
            }
        }

        private void CreateFiles(string path)
        {
            File.Create(path+"\\Test.cs");
            File.Create(path + "\\Test.pdf");
            File.Create(path + "\\Test.txt");
        }
        [Test]
        public void CountCsFiles()
        {
            List<string> expected = new List<string>();
            foreach (var path in paths)
                expected.Add(path + "\\Test.cs");
            expected.RemoveAt(2);
            string[] output = lister.ListFiles("D:\\TestFolder\\");
            Assert.AreEqual(expected.ToArray(), output);
        }
        [Test]
        public void NoValidFileTest()
        {
            Directory.CreateDirectory("D:\\Gej");
            try
            {
                lister.ListFiles("D:\\Gej");

            }
            catch (FileNotFoundException e)
            {
                Directory.Delete("D:\\Gej");
                Assert.Pass();
            }
            Directory.Delete("D:\\Gej");
            Assert.Fail();
        }
        [Test]
        public void InvalidDirectoryTest()
        {
            try
            {
                lister.ListFiles("Nothing");
            }
            catch (DirectoryNotFoundException e)
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
        [OneTimeTearDown]
        public void DeleteDirectory()
        {
            Directory.Delete("D:\\TestFolder\\", true);
        }
    }
}
