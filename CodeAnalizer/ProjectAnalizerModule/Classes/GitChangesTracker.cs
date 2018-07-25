using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalizer
{
    public class GitChangesTracker : IGitChangesTracker
    {
        public Tuple<int, int> ChangedLinesCount()
        {
            throw new NotImplementedException();
        }

        public Tuple<int, int> ChangedLinesCount(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Tuple<int, int> ChangedLinesCount(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public int CommitsCount()
        {
            throw new NotImplementedException();
        }

        public int CommitsCount(DateTime date)
        {
            throw new NotImplementedException();
        }

        public int CommitsCount(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public List<string> MessagesTexts()
        {
            throw new NotImplementedException();
        }

        public List<string> MessagesTexts(DateTime date)
        {
            throw new NotImplementedException();
        }

        public List<string> MessagesTexts(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }
    }
}
