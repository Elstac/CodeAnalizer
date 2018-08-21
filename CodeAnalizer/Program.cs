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
            string msg = "Todays commits: " + GCT.GetChanges(DateTime.Now) + "\n" +
                "This months commits: " + GCT.GetChanges(new DateTime(2018, 7, 1), new DateTime(2018, 7, 31)) + "\n" +
                "All commits: " + GCT.CommitsCount();
            msg = "Today: \n";
            List<string> list = GCT.MessagesTexts();
            foreach (var item in list)
            {
                msg += item + "\n";
            }

            Console.WriteLine(msg);

            //List<string> test = StringEditor.GetLines("dupa\ndupa\nsiur\n");

            //foreach (var item in test)
            //{
            //    Console.WriteLine( item);
            //}

            Console.ReadKey();
            
        }
    }
}
