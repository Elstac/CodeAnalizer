using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;
namespace CodeAnalizer.GitTrackerModule.Classes
{
    class AuthorTracker:GitChangesTracker
    {
        private string name;
        private string email;
        private Commit[] commits;
        private Diff diff;
        public AuthorTracker(string name, string email, Commit[] commits,Diff diff):base()
        {
            this.name = name;
            this.email = email;
            this.commits = commits;
            this.diff = diff;
        }

        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public Commit[] Commits { get => commits; set => commits = value; }

        public override Tuple<int, int> ChangedLinesCount()
        {
            Func<Signature, bool> con = delegate (Signature sig) { return true; };

            return AddChangedLines(con);
        }

        public override Tuple<int, int> ChangedLinesCount(DateRange dateRnage)
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

        public override int CommitsCount()
        {
            return commits.Length;
        }

        public override int CommitsCount(DateRange dateRange)
        {
            int ret = 0;
            foreach (var commit in commits)
                if (commit.Author.When.Date <= dateRange.Begin.Date&& commit.Author.When.Date <= dateRange.End.Date)
                    ret++;
            return ret;
        }

        public override List<string> GetChanges()
        {
            Func<Signature, bool> con = delegate (Signature sig)
            {
                return true;
            };
            return AddChanges(con);
        }

        public override List<string> GetChanges(DateRange dateRange)
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

       

        public override List<string> MessagesTexts()
        {
            Func<Signature, bool> condition = delegate (Signature s)
            {
                return true;
            };
            return GetMessages(condition);
        }

        public override List<string> MessagesTexts(DateRange dateRange)
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
    }
}
