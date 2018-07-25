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
        private string path = "C\\users\\kuba\\source\\repos\\codeanalizer\\codeanalizertests\\testfolder\\testfile.txt";
        GitChangesTracker GCT = new GitChangesTracker();
        [Test]
        public void AddedLinesDailyTest()
        {
            int expected = 4;
            int output = GCT.ChangedLinesCount(new DateTime(2018, 6, 24)).Item1;

            Assert.AreEqual(expected, output);
        }
        [Test]
        public void AddedLinesBracketTest()
        {
            int expected = 3;
            int output = GCT.ChangedLinesCount(new DateTime(2018, 6, 25), new DateTime(2020, 6, 26)).Item1;

            Assert.AreEqual(expected, output);
        }
        [Test]
        public void AllAddedLinesTest()
        {
            int expected = 6;
            int output = GCT.ChangedLinesCount().Item1;

            Assert.AreEqual(expected, output);
        }
        [Test]
        public void CorrectCommitCount()
        {
            int expected = 4;
            Assert.AreEqual(expected, GCT.CommitsCount());
        }
    }
}
