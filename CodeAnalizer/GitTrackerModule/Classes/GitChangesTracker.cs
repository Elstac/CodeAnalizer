using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalizer.GitTrackerModule.Classes
{
    public abstract class GitChangesTracker:IGitChangesTracker
    {
        private List<IGitChangesTracker> children;

        public GitChangesTracker()
        {
            children = new List<IGitChangesTracker>();
        }
        public List<IGitChangesTracker> Children { get => children; }

        public virtual Tuple<int, int> ChangedLinesCount()
        {
            Tuple<int, int> tmp;
            int add = 0, del = 0;
            foreach (var child in children)
            {
                tmp = child.ChangedLinesCount();
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
                tmp = child.ChangedLinesCount(dateRnage);
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

        public void AddChildren(IGitChangesTracker gtc)
        {
            children.Add(gtc);
        }

        public List<AuthorInfo> GetAuthorts()
        {
            List<AuthorInfo> ret = new List<AuthorInfo>();
            foreach (var child in children)
            {
                ret.AddRange(child.GetAuthorts());
            }
            return ret;
        }
    }
}
