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
        Diff diff;
        public BranchCollector(string repoName, string branchName)
        {
            Repository repo = new Repository(repoName);
            diff = repo.Diff;
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
        /// <summary>
        /// Method counts added and deleted lines in all changes in branch which was commited in given time range.
        /// Nothing special
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>Tuple contains: Item1- added lines, Item2- deleted lines</returns>
        public Tuple<int, int> CountChangedLines(DateTime from, DateTime to)
        {
            int added = 0, deleted = 0;
            Patch tmp;
            Commit tmpCom=null;
            DateTime time;
            foreach (var commit in branch.Commits)
            {
                if (tmpCom == null) {
                    tmpCom = commit;
                    break;
                }
                time = commit.Author.When.DateTime;
                if (time < from || time > to)
                    break;
                tmp = diff.Compare<Patch>(commit.Tree, tmpCom.Tree);
                added += tmp.LinesAdded;
                deleted += tmp.LinesDeleted;
                tmpCom = commit;
            }

            return new Tuple<int, int>(added,deleted);
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
