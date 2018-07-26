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
    class GitBranchCollectorTests
    {
        private string path = "c:/users/kuba/source/repos/codeanalizer/codeanalizertests/testfolder";
        BranchCollector BC= new BranchCollector("c:/users/kuba/source/repos/codeanalizer/codeanalizertests/testfolder","master");

        [Test]
        public void BadLoadTest()
        {
            try
            {
                new BranchCollector("gej","opop");
            }
            catch (RepositoryNotFoundException e)
            {
                Assert.Pass();
            }
            Assert.Fail("Method does not throw RepositoryNotFoundException when loading not exsisting repository");
        }
        [Test]
        public void AddedLinesDailyTest()
        {
            int expected = 4;
            int output = BC.CountChangedLines(new DateTime(2018, 6, 24), new DateTime(2018, 6, 24)).Item1;

            Assert.AreEqual(expected, output);
        }
        [Test]
        public void AddedLinesBracketTest()
        {
            int expected = 3;
            int output = BC.CountChangedLines(new DateTime(2018, 6, 25), new DateTime(2020, 6, 26)).Item1;

            Assert.AreEqual(expected, output);
        }
        [Test]
        public void AllAddedLinesTest()
        {
            int expected = 6;
            int output = BC.CountChangedLines().Item1;

            Assert.AreEqual(expected, output);
        }
        [Test]
        public void CommitCountDailyTest()
        {
            int expected = 2;
            Assert.AreEqual(expected, BC.CountCommits(new DateTime(2018,7,24), new DateTime(2018, 7, 24)));
        }
        [Test]
        public void CommitCountRangeTest()
        {
            int expected = 2;
            Assert.AreEqual(expected, BC.CountCommits(new DateTime(2018, 7, 25), new DateTime(2020, 7, 26)));
        }

        [Test]
        public void CountAuthorTest()
        {
            int expected = 4;
            Assert.AreEqual(expected, BC.CountAuthorCommits("Jakub < 1elstac1@gmail.com >"));
        }
        
    }
}
