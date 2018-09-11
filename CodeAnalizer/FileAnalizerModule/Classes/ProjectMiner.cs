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
        private List<FileSetMiner> analizers;

        public List<FileSetMiner> Analizers { get => analizers; set => analizers = value; }

        public ProjectMiner()
        {
            Analizers = new List<FileSetMiner>();
        }

        public ProjectMiner(List<FileSetMiner> analizers)
        {
            Analizers = analizers;
        }

        public ProjectMiner(string[] paths)
        {
            analizers = new List<FileSetMiner>();
            FileSetMiner analizer = new FileSetMiner(paths);

        }

    }
}
