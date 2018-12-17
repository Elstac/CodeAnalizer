using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;
namespace CodeAnalizer.GitTrackerModule.Classes
{
    class AuthorTracker:IGitChangesTracker
    {
        private string name;
        private string email;
        private List<Commit> commits;
        private Diff diff;
        public AuthorTracker(AuthorInfo info,Diff diff):base()
        {
            this.name = info.name;
            this.email = info.email;
            this.commits = info.commits;
            this.diff = diff;
        }

        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public List<Commit> Commits { get => commits; set => commits = value; }

        public  Tuple<int, int> ChangedLinesCount()
        {
            Func<Signature, bool> con = delegate (Signature sig) { return true; };

            return AddChangedLines(con);
        }

        public  Tuple<int, int> ChangedLinesCount(DateRange dateRnage)
        {
            Func<Signature, bool> con = delegate (Signature sig) { return dateRnage.IsInRange(sig.When.Date); };

            return AddChangedLines(con);
        }

        private Tuple<int, int> AddChangedLines(Func<Signature, bool> condition)
        {
            int add = 0, del = 0;
            foreach (var commit in commits)
            {
                if (commit.Parents.ToList().Count > 1 || commit.Parents.ToList().Count < 1)
                    continue;
                if (!condition(commit.Author))
                    continue;
                Patch tmp = diff.Compare<Patch>(commit.Parents.First().Tree, commit.Tree);
                add += tmp.LinesAdded;
                del += tmp.LinesDeleted;
            }
            return new Tuple<int, int>(add, del);
        }

        public  int CommitsCount()
        {
            return commits.Count;
        }

        public  int CommitsCount(DateRange dateRange)
        {
            int ret = 0;
            foreach (var commit in commits)
                if (commit.Author.When.Date <= dateRange.Begin.Date&& commit.Author.When.Date >= dateRange.End.Date)
                    ret++;
            return ret;
        }

        public  List<string> GetChanges()
        {
            Func<Signature, bool> con = delegate (Signature sig)
            {
                return true;
            };
            return AddChanges(con);
        }

        public  List<string> GetChanges(DateRange dateRange)
        {
            Func<Signature, bool> con = delegate (Signature sig)
            {
                return (dateRange.IsInRange(sig.When.Date));
            };
            return AddChanges(con);
        }

        private List<string> AddChanges(Func<Signature, bool> condiditon)
        {
            List<string> ret = new List<string>();
            foreach (var commit in commits)
            {
                if (commit.Parents.ToList().Count != 1)
                    continue;

                if (!condiditon(commit.Author))
                    continue;
                Patch tmp = diff.Compare<Patch>(commit.Parents.First().Tree, commit.Tree);
                ret.Add(commit.Id.ToString() + " " + commit.Author.ToString());
                ret.Add(GitDiffParser.GetChangedLinesText(tmp.Content));
            }
            return ret;
        }

       

        public  List<string> MessagesTexts()
        {
            Func<Signature, bool> condition = delegate (Signature s)
            {
                return true;
            };
            return GetMessages(condition);
        }

        public  List<string> MessagesTexts(DateRange dateRange)
        {
            Func<Signature, bool> condition = delegate (Signature s)
            {
                return (dateRange.IsInRange(s.When.Date));
            };
            return GetMessages(condition);
        }
        private List<string> GetMessages(Func<Signature, bool> condition)
        {
            List<string> ret = new List<string>();
            foreach (var commit in commits)
            {
                if (condition(commit.Author))
                    ret.Add(commit.Id + ": " + commit.Message);
            }
            return ret;
        }

        public List<AuthorInfo> GetAuthorts()
        {
            return new List<AuthorInfo>() { new AuthorInfo(name, email, commits) };
        }
    }
}
