using System;
using System.Collections.Generic;
using System.Linq;
namespace CodeAnalizer
{ 
    public interface IGitChangesTracker
    {
        Tuple<int,int> ChangedLinesCount();
        Tuple<int, int> ChangedLinesCount(DateRange dateRnage);

        List<string> MessagesTexts();
        List<string> MessagesTexts(DateRange dateRange);

        int CommitsCount();
        int CommitsCount(DateRange dateRange);

        List<string> GetChanges();
        List<string> GetChanges(DateRange dateRange);
    }
}
