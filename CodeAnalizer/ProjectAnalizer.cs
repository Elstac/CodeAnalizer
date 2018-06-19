using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalizer
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
    public class ProjectAnalizer
    {
        private List<Analizer> analizers;
        private List<Contributor> contributors;
        
        public ProjectAnalizer(Language lan)
        {
            analizers = new List<Analizer>();
            contributors = new List<Contributor>();
            LanguageSelector.Language = lan;
        }
        /// <summary>
        /// Adds contributorless analizer for grup of files
        /// </summary>
        /// <param name="paths">Paths of files to add</param>
        public void AddFilesSet(string[] paths)
        {
            analizers.Add(new Analizer(paths));
        }
        /// <summary>
        /// Adds new contributor to the project with set of files that belongs to him
        /// </summary>
        /// <param name="name">Name of contributor</param>
        /// <param name="paths">Paths to contributors files</param>
        public void AddContributor(string name, string[] paths)
        {
            Analizer tmp = new Analizer(paths);
            contributors.Add(new Contributor(name,tmp));
            analizers.Add(tmp);
        }
        /// <summary>
        /// Returns total number of line in project
        /// </summary>
        /// <returns></returns>
        public int TotalLines()
        {
            int ret=0;
            foreach (Analizer item in analizers)
            {
                ret+= item.GetLinesCount();
            }
            return ret;
        }
        /// <summary>
        /// Retutns total number of characters in project
        /// </summary>
        /// <returns></returns>
        public int TotalCharacters()
        {
            int ret = 0;
            foreach (Analizer item in analizers)
            {
                ret += item.GetCharacktersCount();
            }
            return ret;
        }
        /// <summary>
        /// Counts number of usings/imports etc.
        /// </summary>
        /// <returns>Number of lines containig usings/imports etc.</returns>
        public int TotalUsings()
        {
            int ret = 0;
            foreach (Analizer item in analizers)
            {
                ret += item.GetUsingsCount();
            }
            return ret;
        }
        /// <summary>
        /// List all files with thier statistics: lines, characters, usings, empty lines, characters/line...
        /// </summary>
        /// <returns>string containig those informations</returns>
        public string ListAllFiles()
        {
            StringBuilder ret = new StringBuilder();
            foreach (Analizer analizer in analizers)
            {
                ret.Append( analizer.AnalizeFiles());
            }
            return ret.ToString();
        }
        /// <summary>
        /// Creates contribution log of specified type
        /// </summary>
        /// <param name="type">Type of generation log</param>
        /// <returns>String contains generated log</returns>
        public string Contribution(ContributionType type)
        {
            if (contributors.Count <= 0)
                throw new IndexOutOfRangeException("Contributors list is empty");

            StringBuilder ret = new StringBuilder();
            if (type == ContributionType.Numbers)
            {
                foreach (Contributor contributor in contributors)
                {
                    int lines = contributor.Analizer.GetLinesCount(), chars = contributor.Analizer.GetCharacktersCount(),
                        commentsL = contributor.Analizer.GetCommentLines();
                    ret.Append(contributor.Name + ":\n" + "Lines: " + lines +
                        " Characters: " + chars + 
                        " Empty Lines: " + contributor.Analizer.GetEmptyLines() +
                        " Character per Line: "+(float)chars/lines +
                        " Comments lines: " + commentsL + "\n");
                }
            }
            else if (type == ContributionType.Procentage)
            {
                float sumaL = 0.0f, sumaC = 0.0f;
                foreach (Contributor contributor in contributors)
                {
                    sumaC += contributor.Analizer.GetCharacktersCount();
                    sumaL += contributor.Analizer.GetLinesCount();
                }
                foreach (Contributor contributor in contributors)
                {
                    ret.Append(contributor.Name + ":\n" + "Lines : " + Math.Round(contributor.Analizer.GetLinesCount() / sumaL * 100) + "%" +
                        " Characters: " + Math.Round(contributor.Analizer.GetCharacktersCount() / sumaC * 100) + "% Empty Lines: "+
                        Math.Round( contributor.WasteParam)+"% "+ "\n");
                }
            }
            else if (type == ContributionType.Files)
            {
                foreach (Contributor contributor in contributors)
                {
                    ret.Append(contributor.Name+":\n"+ contributor.Analizer.AnalizeFiles());
                }
            }
            return ret.ToString();
        }

    }
}
