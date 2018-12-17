using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using LibGit2Sharp;
using CodeAnalizer.GitTrackerModule.Classes;
namespace CodeAnalizerTests.GitTrackerModuleTests
{
    [TestFixture]
    class RepoTrackerTest
    {
        private const string pathToRepo = "D:\\AnalizerTest\\Test";
        private RepoTracker repoTracker;

        [OneTimeSetUp]
        public void LoadRepo()
        {
            repoTracker = new RepoTracker(pathToRepo);
        }

        [Test]
        public void CountAllCommits()
        {
            int expected = 5;
            Assert.AreEqual(expected, repoTracker.CommitsCount());
        }

        [Test]
        public void CountCommitsInRange()
        {
            int expected = 1;
            Assert.AreEqual(expected, repoTracker.CommitsCount(new CodeAnalizer.DateRange(new DateTime(2019,10,10))));
        }

        [Test]
        public void CountAllChangedLines()
        {
            Tuple<int,int> expected = new Tuple<int, int>(6,2);
            Assert.AreEqual(expected, repoTracker.ChangedLinesCount());
        }
        [Test]
        public void CountChangedLinesInRange()
        {
            Tuple<int, int> expected = new Tuple<int, int>(4, 0);
            Assert.AreEqual(expected, repoTracker.ChangedLinesCount(new CodeAnalizer.DateRange(new DateTime(2019, 10, 10))));
        }

        [Test]
        public void GetAllMessages()
        {
            List<string> expected = new List<string>()
            {
                "Marius\n",
                "Gene\n",
                "Gunther\n",
                "Rhode\n",
                "Initial commit\n"
            };
            expected.Sort();
            List<string> output = repoTracker.MessagesTexts();
            for (int i = 0; i < output.Count; i++)
            {
                string message = output[i];
                int index = message.IndexOf(":") + 2;
                output[i] = message.Substring(index);
            }
            output.Sort();
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void GetMessagesInRange()
        {
            List<string> expected = new List<string>()
            {
                "Marius\n",
                "Gene\n",
                "Gunther\n",
                "Initial commit\n"
            };
            expected.Sort();
            DateTime date = new DateTime(2018, 9, 10);
            List<string> output = repoTracker.MessagesTexts(new CodeAnalizer.DateRange(date));
            for (int i =0; i<output.Count;i++)
            {
                string message = output[i];
                int index = message.IndexOf(":") + 2;
                output[i] = message.Substring(index);
            }
            output.Sort();
            Assert.AreEqual(expected, output);
        }
        
        [Test]
        public void GetAuthorsTest()
        {
            List<AuthorInfo> output = repoTracker.GetAuthorts();

            List<AuthorInfo> expected = new List<AuthorInfo>()
            {
                new AuthorInfo("Jan Pawel II","jp2@gmd.pl"),
                new AuthorInfo("Jakub","1elstac1@gmail.com")
            };
            Assert.AreEqual(expected, output);
        }
    }
}
