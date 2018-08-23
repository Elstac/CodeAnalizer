using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace CodeAnalizer
{
    /// <summary>
    /// Class responsible for ghatering data from set of files. "Dataminer for multiple files" 
    /// and Pair filenames with stats.
    /// </summary>
    public class FileAnalizer
    {
        private DataMiner dataminer = new DataMiner(LanguageSelector.GetMethodTemlate(),LanguageSelector.GetNamePosition());
        private List<string> paths;
        
        public FileAnalizer(string[] paths)
        {
            this.paths = new List<string>();
            AddFiles(paths);
        }
        
        public void AddFiles(string[] paths)
        {
            this.paths.AddRange(paths);
        }
        
        /// <summary>
        /// Lists all files in file set with thier statistics( lines, empty lines, chracters...).
        /// </summary>
        /// <returns>String containing list of files</returns>
        public string AnalizeFiles()
        {
            StringBuilder ret = new StringBuilder();
            foreach (var path in paths)
            {
                
                ret.Append(path+"\n");
                ret.Append("| Lines: "+ dataminer.CountLines(path));
                ret.Append(" | Empty Lines: "+dataminer.CountEmpty(path));
                ret.Append(" | Characters: "+dataminer.CountCharacters(path));
                ret.Append(" | Methods: " + dataminer.CountMethods(path) + " |\n");

            }
            
            return ret.ToString();
        }

        public int GetLinesCount()
        {
            int ret = 0;
            foreach (var path in paths)
            {
                ret += dataminer.CountLines(path);
            }
            return ret;
        }

        public int GetEmptyLines()
        {
            int ret = 0;
            foreach (var path in paths)
            {
                ret += dataminer.CountEmpty(path);
            }
            return ret;
        }
        /// <summary>
        /// Counts all characters in file set.
        /// </summary>
        /// <returns></returns>
        public int GetCharacktersCount()
        {
            int ret = 0;
            foreach (var path in paths)
            {
                ret += dataminer.CountCharacters(path);
            }
            return ret;
        }

        public int GetUsingsCount()
        {
            int ret = 0;
            foreach (var path in paths)
            {
                ret += dataminer.CountUsings(path);
            }
            return ret;
        }
        /// <summary>
        /// Counts all lines cintaing a comments.
        /// </summary>
        /// <returns>Number of comment lines</returns>
        public int GetCommentLines()
        {
            int ret = 0;
            foreach (var path in paths)
            {
                ret += dataminer.CountComments(path);
            }
            return ret;
        }
        /// <summary>
        /// Counts all methods in file set. Temprorary doesnt count: static, override, abstract, virtual methods.
        /// </summary>
        /// <returns>Number of methods</returns>
        public int GetMethodsCount()
        {
            int ret = 0;
            foreach (var path in paths)
            {
                ret += dataminer.CountMethods(path);
            }
            return ret;
        }
        /// <summary>
        /// Finds largest file in file set.
        /// </summary>
        /// <returns>String contains path to finded file or meesage saing that that file doesnt exist</returns>
        public string GetLargestFile()
        {
            string ret="There is no largest file";
            int maxChar = 0,tmp;
            foreach (var path in paths)
            {
                tmp = dataminer.CountCharacters(path);
                if (maxChar < tmp)
                {
                    maxChar = tmp;
                    ret = path;
                }
            }

            return ret;
        }
        /// <summary>
        /// Finds smallest file in file set.
        /// </summary>
        /// <returns>String contains path to finded file or meesage saing that file doesnt exist</returns>
        public string GetSmallestFile()
        {
            string ret = "There is no smallest file";

            int minChar = dataminer.CountCharacters(paths[0]), tmp;
            foreach (var path in paths)
            {
                tmp = dataminer.CountCharacters(path);
                if (minChar > tmp)
                {
                    minChar = tmp;
                    ret = path;
                }
            }

            return ret;
        }

        public bool RemoveFile(string path)
        {
            foreach (var file in paths)
            {
                if (file == path)
                {
                    paths.Remove(path);
                    if (paths.Count == 0)
                        throw new EmptyAnalizerException();
                    return true;
                }
            }
            return false;
        }
    }
}
