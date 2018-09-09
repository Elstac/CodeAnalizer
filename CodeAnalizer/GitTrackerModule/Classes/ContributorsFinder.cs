using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;
namespace CodeAnalizer.GitTrackerModule.Classes
{
    class ContributorsFinder
    {

        public static List<string> FindContributors(Repository repository)
        {
            List<string> ret = new List<string>();

            Commit[] commits = repository.Commits.ToArray();
            foreach (var commit in commits)
            {
                if (ret.Contains(commit.Author.Name))
                    continue;

                ret.Add(commit.Author.Name);
            }
            return ret;
        }
    }
}
