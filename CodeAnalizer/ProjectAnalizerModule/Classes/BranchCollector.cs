using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeAnalizer;
using LibGit2Sharp;
namespace CodeAnalizer
{
    /// <summary>
    /// Clas responsible for gathering informations from given branch( include: count of commits, added lines,
    /// deleted lines, when commits were commited etc.)
    /// </summary>
    public class BranchCollector : IGitBranchCollector
    {
        Branch branch;
        public BranchCollector(string repoPath, string branchName)
        {
            Repository repo = new Repository(repoPath);
            branch = repo.Branches[branchName];
        }
        public List<string> AuthorsLines(string authorName)
        {
            throw new NotImplementedException();
        }

        public int CountAuthorCommits(string authorName)
        {
            throw new NotImplementedException();
        }

        public Tuple<int, int> CountChangedLines(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public Tuple<int, int> CountChangedLines()
        {
            throw new NotImplementedException();
        }

        public int CountCommits(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public int CountCommits()
        {
            throw new NotImplementedException();
        }
    }
}
