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
            string path = Directory.GetCurrentDirectory() + "/../../..";

            GitChangesTracker GCT = new GitChangesTracker(path);
            string msg = "Todays commits: "+ GCT.CommitsCount(DateTime.Now) + "\n" +
                "This months commits: " + GCT.CommitsCount(new DateTime(2018, 7, 1), new DateTime(2018, 7, 31)) + "\n" +
                "All commits: " + GCT.CommitsCount();
            Console.WriteLine(msg);

            Console.ReadKey();
            
        }
    }
}
