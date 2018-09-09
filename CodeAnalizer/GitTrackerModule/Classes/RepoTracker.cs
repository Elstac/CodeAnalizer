using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LibGit2Sharp;
namespace CodeAnalizer.GitTrackerModule.Classes
{
    public class RepoTracker: GitChangesTracker
    {
        public RepoTracker(string pathToRepo)
        {
            Repository repo = new Repository(pathToRepo);
            string[] conts = ContributorsFinder.FindContributors(repo).ToArray();
            AuthorTracker tmp;
            foreach (var cont in conts)
            {
                
            }
        }
    }
}
