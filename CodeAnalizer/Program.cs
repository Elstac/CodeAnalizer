using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LibGit2Sharp;
using CodeAnalizer.GitTrackerModule.
    Classes;
namespace CodeAnalizer
{
    class Program
    {
        static void Main(string[] args)
        {
            string path ="D:\\Documents\\Projekty\\CodeAnalizerGUI";

            RepoTracker GCT = new RepoTracker(path);
            string msg = "Todays commits: " + GCT.CommitsCount(new DateRange( DateTime.Now)) + "\n" +
                "This months commits: " + GCT.CommitsCount(new DateRange(new DateTime(2018, 7, 1), new DateTime(2018, 7, 31))) + "\n" +
                "All commits: " + GCT.CommitsCount();
            Console.WriteLine(msg);
            msg = "Today: \n";
            //List<string> list = GCT.GetChanges(DateTime.Today);
            //foreach (var item in list)
            //{
            //    msg += item + "\n";
            //}
            var range = new DateRange(new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, DateTime.Now.Day),DateTime.Now);
            msg += "+ " + GCT.ChangedLinesCount(range).Item1 + "\n- " + GCT.ChangedLinesCount(range).Item2;
            Console.WriteLine(msg);
            //List<string> test = StringEditor.GetLines("dupa\ndupa\nsiur\n");

            //foreach (var item in test)
            //{
            //    Console.WriteLine(item);
            //}0


            Console.ReadKey();
            
        }
    }
}
