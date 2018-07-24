using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;
namespace CodeAnalizer
{
    class Tracker
    {
        Repository repo;
        BranchCollection branches;

        public Tracker(string path)
        {
            repo = new Repository(path);
            branches = repo.Branches;
        }

        public string CheckBranchChanges(string branchName)
        {
            Branch toCheck = branches[branchName];
            string ret = "";
            List<Commit> commits = toCheck.Commits.ToList();

            var path=repo.Diff.Compare<Patch>(commits[1].Tree, commits[0].Tree);
            return path.Content;
        }


    }
}
