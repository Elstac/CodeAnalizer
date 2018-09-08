using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalizer.GitTrackerModule.Classes
{
    public abstract class GitChangesTracker
    {
        private List<GitChangesTracker> children;

        public GitChangesTracker()
        {
            children = new List<GitChangesTracker>();
        }
        protected List<GitChangesTracker> Children { get => children; set => children = value; }

        public virtual Tuple<int, int> ChangedLinesCount()
        {
            Tuple<int, int> tmp;
            int add = 0, del = 0;
            foreach (var child in children)
            {
                tmp = ChangedLinesCount();
                add += tmp.Item1;
                del += tmp.Item2;
            }
            return new Tuple<int, int>(add, del);
        }

        public virtual Tuple<int, int> ChangedLinesCount(DateRange dateRnage)
        {
            Tuple<int, int> tmp;
            int add = 0, del = 0;
            foreach (var child in children)
            {
                tmp = ChangedLinesCount(dateRnage);
                add += tmp.Item1;
                del += tmp.Item2;
            }
            return new Tuple<int, int>(add, del);
        }

        public virtual int CommitsCount()
        {
            int ret = 0;
            foreach (var child in children)
            {
                ret += child.CommitsCount();
            }
            return ret;
        }

        public virtual int CommitsCount(DateRange dateRange)
        {
            int ret = 0;
            foreach (var child in children)
            {
                ret += child.CommitsCount(dateRange);
            }
            return ret;
        }

        public virtual List<string> GetChanges()
        {
            List<string> ret = new List<string>();
            foreach (var child in children)
                ret.AddRange(child.GetChanges());
            return ret;
        }

        public virtual List<string> GetChanges(DateRange dateRange)
        {
            List<string> ret = new List<string>();
            foreach (var child in children)
                ret.AddRange(child.GetChanges(dateRange));
            return ret;
        }

        public virtual List<string> MessagesTexts()
        {
            List<string> ret = new List<string>();
            foreach (var child in children)
                ret.AddRange(child.MessagesTexts());
            return ret;
        }

        public virtual List<string> MessagesTexts(DateRange dateRange)
        {
            List<string> ret = new List<string>();
            foreach (var child in children)
                ret.AddRange(child.MessagesTexts(dateRange));
            return ret;
        }

        public void AddChildren(GitChangesTracker gtc)
        {
            children.Add(gtc);
        }
    }
}
