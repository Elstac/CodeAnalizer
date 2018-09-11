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
        private List<FileSetAnalizer> analizers;

        public List<FileSetAnalizer> Analizers { get => analizers; set => analizers = value; }

        public ProjectMiner()
        {
            Analizers = new List<FileSetAnalizer>();
        }

        public ProjectMiner(List<FileSetAnalizer> analizers)
        {
            Analizers = analizers;
        }

        public ProjectMiner(string[] paths)
        {
            analizers = new List<FileSetAnalizer>();
            FileSetAnalizer analizer = new FileSetAnalizer(paths);

        }

    }
}
