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
            string[] files;
            Analizer[] analizers = new Analizer[3];
            Lister lister = new Lister( new string[] { ".cs" });
            ProjectAnalizer kozak = new ProjectAnalizer(Language.Csharp);
            files = lister.listFiles("D:/AnalizerTest/Kuba");
            kozak.AddContributor("Kuba", files);
            files = lister.listFiles("D:/AnalizerTest/Piecia");
            kozak.AddContributor("Piecia", files);
            files = lister.listFiles("D:/AnalizerTest/Michal");
            kozak.AddContributor("Michal", files);

            //ProjectAnalizer second = new ProjectAnalizer();
            //second.AddFilesSet(lister.listFiles("../.."));
            //Console.WriteLine("Pliki: " + second.ListAllFiles());

            Console.WriteLine("File contribution:\n{0}",kozak.Contribution(ContributionType.Files));
            Console.WriteLine("Contribution:\n a) in numbers:\n{0}\n b) procentage:\n{1}",kozak.Contribution(ContributionType.Numbers),kozak.Contribution(ContributionType.Procentage));
            //Console.WriteLine("Project Numbers in total:\nL: "+kozak.TotalLines()+" C: "+kozak.TotalCharacters()+" Usings "+kozak.TotalUsings());
            //Console.WriteLine("Project II:\nL: " + second.TotalLines() + " C: " + second.TotalCharacters()+" Usings " + second.TotalUsings());
            Console.ReadKey();
        }
    }
}
