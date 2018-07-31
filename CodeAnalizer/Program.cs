using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LibGit2Sharp;
namespace CodeAnalizer
{
    class Program
    {
        static void Main(string[] args)
        {
            Repository repo = new Repository("c:/users/kuba/source/repos/codeanalizer/codeanalizertests/testfolder");

            List<Commit> l= repo.Commits.ToList()[0].Parents.ToList();
            Console.WriteLine(l);
            Console.ReadKey();
            
        }
    }
}
