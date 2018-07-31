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
    class GitChangesTrackerTests
    {
        private string path = "c:/users/kuba/source/repos/codeanalizer/codeanalizertests/testfolder";
        GitChangesTracker GCT= new GitChangesTracker("c:/users/kuba/source/repos/codeanalizer/codeanalizertests/testfolder");

        [Test]
        public void BadLoadTest()
        {
            try
            {
                new GitChangesTracker("gej");
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
            int expected = 3;
            int output = GCT.ChangedLinesCount(new DateTime(2018, 7, 24)).Item1;

            Assert.AreEqual(expected, output);
        }
        [Test]
        public void AddedLinesRangeTest()
        {
            int expected = 5;
            int output = GCT.ChangedLinesCount(new DateTime(2018, 7, 25), new DateTime(2020, 6, 26)).Item1;

            Assert.AreEqual(expected, output);
        }
        [Test]
        public void AllAddedLinesTest()
        {
            int expected = 9;
            int output = GCT.ChangedLinesCount().Item1;

            Assert.AreEqual(expected, output);
        }
        [Test]
        public void CommitCountDailyTest()
        {
            int expected = 2;
            Assert.AreEqual(expected, GCT.CommitsCount(new DateTime(2018,7,24)));
        }
        [Test]
        public void CommitCountRangeTest()
        {
            int expected = 4;
            Assert.AreEqual(expected, GCT.CommitsCount(new DateTime(2018, 7, 25), new DateTime(2020, 7, 26)));
        }

        [Test]
        public void CountAuthorTest()
        {
            int expected = 6;
            Assert.AreEqual(expected, GCT.CountAuthorCommits("Jakub <1elstac1@gmail.com>"));
        }

        [Test]
        public void GetChangesDailyTest()
        {
            List<string> expected = new List<string>
            {
                "+TwojaStaraGej",
                "+Kurwa mac jebac psy kierwa",
                "+Nie ufaj kolegom zaufaj policjantom"
            };

            Assert.AreEqual(expected, GCT.GetChanges());
        }
    }
}
