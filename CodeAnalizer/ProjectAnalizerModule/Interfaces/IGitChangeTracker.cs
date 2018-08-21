using System;
using System.Collections.Generic;
using System.Linq;
namespace CodeAnalizer
{ 
    interface IGitChangesTracker
    {
        Tuple<int,int> ChangedLinesCount();
        Tuple<int, int> ChangedLinesCount(DateTime date);
        Tuple<int, int> ChangedLinesCount(DateTime from, DateTime to);

        List<string> MessagesTexts();
        List<string> MessagesTexts(DateTime date);
        List<string> MessagesTexts(DateTime from, DateTime to);

        int CommitsCount();
        int CommitsCount(DateTime date);
        int CommitsCount(DateTime from, DateTime to);

        int CountAuthorCommits(string authorName);

        List<string> GetChanges();
        List<string> GetChanges(DateTime date);
        List<string> GetChanges(DateTime from, DateTime to);
        List<string> GetChanges(string author);
    }
}
