using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace CodeAnalizer
{
    class Program
    {
        static void Main(string[] args)
        {
            Tracker tr = new Tracker("C:\\Users\\Kuba\\source\\repos\\CodeAnalizer");
            Console.WriteLine(tr.CheckBranchChanges("GitTracker"));
            Console.ReadKey();
        }
    }
}
