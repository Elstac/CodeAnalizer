using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;
using System.IO;
namespace CodeAnalizer
{
    /// <summary>
    /// Class responsible for gathering data from whole repo( n# of commits, lines chnged in given time range,
    /// Known issues: changes in first commit isnt count to n# of changed lines
    /// </summary>
    public class GitChangesTracker : IGitChangesTracker
    {
        private List<Commit> commits;
        private List<BranchCollector> branches;
        private Diff diff;
        /// <summary>
        /// Initialize class, throws _RepositoryNotFoundException_ when was given wrong path, NS
        /// </summary>
        /// <param name="pathToRepo"></param>
        public GitChangesTracker(string pathToRepo)
        {
            if (!Directory.Exists(pathToRepo + "/.git"))
                throw new RepositoryNotFoundException("There is no repo");

            branches = new List<BranchCollector>();
            Repository repo = new Repository(pathToRepo);

            commits = repo.Commits.ToList();
            diff = repo.Diff;

            foreach (var branch in repo.Branches)
                branches.Add(new BranchCollector(pathToRepo, branch.FriendlyName));
        }
        public Tuple<int, int> ChangedLinesCount()
        {
            int added = 0, deleted = 0;
            foreach (var commit in commits)
                AddChangedLines(ref added, ref deleted, commit);

            return new Tuple<int, int>(added, deleted) ;
        }

        public Tuple<int, int> ChangedLinesCount(DateTime date)
        {
            int added = 0, deleted = 0;
            foreach (var commit in commits)
            {
                if (commit.Author.When.Date != date.Date)
                    continue;
                AddChangedLines(ref added, ref deleted, commit);
            }

            return new Tuple<int, int>(added, deleted);
        }

        public Tuple<int, int> ChangedLinesCount(DateTime from, DateTime to)
        {
            int added = 0, deleted = 0;
            from = from.Date; to = to.Date;

            foreach (var commit in commits)
            {
                DateTime time = commit.Author.When.Date;
                if ( time<from||time>to)
                    continue;

                AddChangedLines(ref added, ref deleted, commit);
            }

            return new Tuple<int, int>(added, deleted);
        }

        private void AddChangedLines(ref int add, ref int del,Commit commit)
        {
            if (commit.Parents.ToList().Count > 1 || commit.Parents.ToList().Count < 1)
                return;

            Patch tmp = diff.Compare<Patch>(commit.Parents.First().Tree, commit.Tree);
            add += tmp.LinesAdded;
            del += tmp.LinesDeleted;
        }
        public int CommitsCount()
        {
            return commits.Count;
        }

        public int CommitsCount(DateTime date)
        {
            int ret = 0;
            foreach (var commit in commits)
                if (commit.Author.When.Date == date.Date)
                    ret++;
            return ret;
        }

        public int CommitsCount(DateTime from, DateTime to)
        {
            int ret = 0;
            foreach (var commit in commits)
                if (commit.Author.When.Date >= from && commit.Author.When.Date<=to)
                    ret++;
            return ret;
        }

        public int CountAuthorCommits(string authorName)
        {
            int ret = 0;
            foreach (var commit in commits)
                if (commit.Author.ToString() == authorName)
                    ret++;

            return ret;
        }

        public List<string> GetChanges()
        {
            Func<Signature, bool> con = delegate (Signature sig)
             {
                 return true;
             };
            return AddChanges(con);
        }

        public List<string> GetChanges(DateTime date)
        {
            Func<Signature, bool> con = delegate (Signature sig)
            {
                return (sig.When.Date == date.Date);
            };
            return AddChanges(con);
        }

        public List<string> GetChanges(DateTime from, DateTime to)
        {
            Func<Signature, bool> con = delegate (Signature sig)
            {
                return (sig.When.Date >= from.Date&& sig.When.Date <= to.Date);
            };
            return AddChanges(con);
        }
        /// <summary>
        /// Method add strings containing changed lines from commit which fulfill given condition
        /// </summary>
        ///<param name="condiditon">Logic func represents condition</param>
        private List<string> AddChanges(Func<Signature,bool> condiditon)
        {
            List<string> ret = new List<string>();
            foreach (var commit in commits)
            {
                if (commit.Parents.ToList().Count > 1 || commit.Parents.ToList().Count < 1)
                    return null;

                if (!condiditon(commit.Author))
                    return null;
                Patch tmp = diff.Compare<Patch>(commit.Parents.First().Tree, commit.Tree);
                ret.Add(commit.Id.ToString() + " " + commit.Author.ToString());
                ret.Add(ParseContent(tmp.Content));
            }
            return ret;
        }

        private string ParseContent(string content)
        {
            string ret = "";

            List<string> tmp= StringEditor.GetLines(content);

            foreach (var item in tmp)
            {
                string text = StringEditor.GetRawText(item);
                if (text.Length == 0)
                    continue;
                if (text.First() == '+' || text.First() == '-')
                    if (text.Length==1||(text[1] != '+' && text[1] != '-'))
                        ret += text+"\n";
            }
            return ret;
        }

        public List<string> MessagesTexts()
        {
            Func<Signature, bool> condition = delegate (Signature s)
             {
                 return true;
             };
            return GetMessages(condition);
        }

        public List<string> MessagesTexts(DateTime date)
        {
            Func<Signature, bool> condition = delegate (Signature s)
            {
                return (s.When.Date == date.Date);
            };
            return GetMessages(condition);
        }

        public List<string> MessagesTexts(DateTime from, DateTime to)
        {
            Func<Signature, bool> condition = delegate (Signature s)
            {
                return (s.When.Date < to.Date && s.When.Date > from.Date);
            };
            return GetMessages(condition);
        }

        private List<string> GetMessages(Func<Signature,bool> condition)
        {
            List<string> ret = new List<string>();
            foreach (var commit in commits)
            {
                if(condition(commit.Author))
                    ret.Add(commit.Id + ": " + commit.Message);
            }
            return ret;
        }

        public List<string> GetChanges(string author)
        {
            throw new NotImplementedException();
        }
    }
}