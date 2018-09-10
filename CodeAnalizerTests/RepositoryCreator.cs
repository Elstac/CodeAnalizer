using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;
namespace CodeAnalizerTests
{
    public class RepositoryCreator
    {
        private Repository repo;

        public RepositoryCreator()
        {
            repo = new Repository();
            
        }
    }
}
