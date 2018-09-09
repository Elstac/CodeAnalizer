using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;
namespace CodeAnalizer.GitTrackerModule.Classes
{
    /// <summary>
    /// Static class which gathers list of contributors from given repository
    /// </summary>
    public static class ContributorsFinder
    {

        public static List<AuthorInfo> FindContributors(Repository repository)
        {
            List<AuthorInfo> ret = new List<AuthorInfo>();
            AuthorInfo tmp;
            Commit[] commits = repository.Commits.ToArray();
            foreach (var commit in commits)
            {
                tmp = new AuthorInfo(commit.Author.Name, commit.Author.Email);
                if (ret.Contains(tmp))
                    continue;

                ret.Add(tmp);
            }
            return ret;
        }
    }
}
