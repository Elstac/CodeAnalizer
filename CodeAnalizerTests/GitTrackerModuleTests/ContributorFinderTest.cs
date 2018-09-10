using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeAnalizer.GitTrackerModule.Classes;
using NUnit.Framework;
using LibGit2Sharp;
namespace CodeAnalizerTests.GitTrackerModuleTests
{
    [TestFixture]
    class ContributorFinderTest
    {
        private const string pathToRepo = "D:\\AnalizerTest\\Test";
        private Repository repo;
        private AuthorInfo[] outputA;
        [OneTimeSetUp]
        public void LoadRepo()
        {
            repo = new Repository(pathToRepo);
            outputA = ContributorsFinder.FindContributors(repo).ToArray();
        }
        [Test]
        public void ContributorsNames()
        {
            List<string> expected = new List<string>
            {
                "Jan Pawel II",
                "Jakub"
            };
            List<string> output = new List<string>();
            foreach (var author in outputA)
            {
                output.Add(author.name);
            }
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void ContributorsEmails()
        {
            List<string> expected = new List<string>
            {
                "jp2@gmd.pl",
                "1elstac1@gmail.com"
            };
            List<string> output = new List<string>();
            foreach (var author in outputA)
            {
                output.Add(author.email);
            }
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void ContributorsCommitsCount()
        {
            List<int> expected = new List<int>
            {
                1,4
            };
            List<int> output = new List<int>();
            foreach (var author in outputA)
            {
                output.Add(author.commits.Count);
            }
            Assert.AreEqual(expected, output);
        }

    }
}
