using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;
namespace CodeAnalizer
{
    interface IGitBranchCollector
    {
        Tuple<int,int> CountChangedLines(DateTime from, DateTime to);
        Tuple<int, int> CountChangedLines();

        int CountCommits(DateTime from, DateTime to);
        int CountCommits();

        int CountAuthorsCommits(string authorName);
        List<string> AuthorsLines(string authorName);
    }
}
