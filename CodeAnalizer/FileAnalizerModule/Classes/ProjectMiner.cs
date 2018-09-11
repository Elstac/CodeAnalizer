using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CodeAnalizer.FileAnalizerModule.Classes
{
    public enum ContributionType
    {
        Numbers,
        Procentage,
        Files,
    }
    /// <summary>
    /// Class responsible for gathering stats for whole poject.
    /// </summary>
    public class ProjectMiner:FileMiner
    { 

        public ProjectMiner():base()
        {
            
        }

        public ProjectMiner(List<FileSetMiner> analizers) : base()
        {
            Children.AddRange(analizers);
        }

        public ProjectMiner(string[] paths) : base()
        {
            FileSetMiner analizer = new FileSetMiner(paths);
            Children.Add(analizer);
        }

    }
}
