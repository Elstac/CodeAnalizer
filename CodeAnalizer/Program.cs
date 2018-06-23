﻿using System;
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
            //string[] files;
            //Analizer[] analizers = new Analizer[3];
            //Lister lister = new Lister( new string[] { ".cs" },new string[] {"bin","Properties","obj" });
            //ProjectAnalizer kozak = new ProjectAnalizer(Language.Csharp);
            //files = lister.ListFiles("D:/AnalizerTest/Kuba");
            //kozak.AddContributor("Kuba", files);
            //files = lister.ListFiles("D:/AnalizerTest/Piecia");
            //kozak.AddContributor("Piecia", files);
            //files = lister.ListFiles("D:/AnalizerTest/Michal");
            //kozak.AddContributor("Michal", files);

            //files = lister.ListFiles("../..");
            //ProjectAnalizer self = new ProjectAnalizer(Language.Csharp);
            //self.AddContributor("Kuba", files);
            //Console.WriteLine(self.ListAllFiles());
            //Console.WriteLine("CodeAnalizer Numbers in total:\nL: " + self.TotalLines() + "| C: " + self.TotalCharacters() + "| Usings " + self.TotalUsings());
            //Console.WriteLine("Secondary stats:\n" + self.GetSecondaryStats());
            //ProjectAnalizer second = new ProjectAnalizer();
            //second.AddFilesSet(lister.listFiles("../.."));
            //Console.WriteLine("Pliki: " + second.ListAllFiles());

            //Console.WriteLine("File contribution:\n{0}",kozak.Contribution(ContributionType.Files));
            //Console.WriteLine("Contribution:\n a) in numbers:\n{0}\n b) procentage:\n{1}", kozak.Contribution(ContributionType.Numbers), kozak.Contribution(ContributionType.Procentage));
            //Console.WriteLine("Secondary statistics:\n" + kozak.GetSecondaryStats());
            //Console.WriteLine("Project Numbers in total:\nL: "+kozak.TotalLines()+" C: "+kozak.TotalCharacters()+" Usings "+kozak.TotalUsings());
            //Console.WriteLine("Project II:\nL: " + second.TotalLines() + " C: " + second.TotalCharacters()+" Usings " + second.TotalUsings());
            string s = "";
            for (int i = 33; i < 127; i++)
            {
                s += " " + (char)i;
                if ((i - 32) % 10 == 0)
                {
                    Console.WriteLine(s);
                    s = "";
                }
            }
            Console.WriteLine(s);
            s = "";
            char key = 'a';
            for (int i = 33 + key; i < 127 + key; i++)
            {
                s += " " + (char)Encrypter.NumberToRange(i,34,126);
                if ((i - 32 - key) % 10 == 0)
                {
                    Console.WriteLine(s);
                    s = "";
                }
            }
            Console.WriteLine(s);
            Console.ReadKey();
        }
    }
}
