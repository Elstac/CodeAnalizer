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
        List<Commit> commits;
        Diff diff;
        public BranchCollector(string repoName, string branchName)
        {
            Repository repo = new Repository(repoName);
            diff = repo.Diff;
            branch = repo.Branches[branchName];
            commits = branch.Commits.ToList();
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
            foreach (var commit in commits)
            {
                if (tmpCom == null) {
                    tmpCom = commit;
                    continue;
                }
                time = tmpCom.Author.When.DateTime.Date;
                if (!CheckCommitDate(tmpCom, from,to))
                {
                    tmpCom = commit;
                    continue;
                }
                tmp = diff.Compare<Patch>(commit.Tree, tmpCom.Tree);
                added += tmp.LinesAdded;
                deleted += tmp.LinesDeleted;
                tmpCom = commit;
            }

            return new Tuple<int, int>(added,deleted);
        }

        public Tuple<int, int> CountChangedLines()
        {
            DateTime first, last;
            first = commits.Last().Author.When.Date;
            last = commits[0].Author.When.Date;

            return CountChangedLines(first, last);
        }

        public int CountCommits(DateTime from, DateTime to)
        {
            int ret = 0;
            
            foreach (var commit in commits)
                if (CheckCommitDate(commit, from, to))
                    ret++;

            return ret;
        }

        public int CountCommits()
        {
            return commits.Count;
        }

        private bool CheckCommitDate(Commit commit, DateTime from,DateTime to)
        {
            DateTime time= commit.Author.When.DateTime.Date;
            return !(time.Date < from.Date || time.Date > to.Date);
        }
    }
}
