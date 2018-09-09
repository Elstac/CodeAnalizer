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
            return base.ChangedLinesCount();
        }

        public override Tuple<int, int> ChangedLinesCount(DateRange dateRnage)
        {
            return base.ChangedLinesCount(dateRnage);
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
            return base.MessagesTexts();
        }

        public override List<string> MessagesTexts(DateRange dateRange)
        {
            return base.MessagesTexts(dateRange);
        }
    }
}
