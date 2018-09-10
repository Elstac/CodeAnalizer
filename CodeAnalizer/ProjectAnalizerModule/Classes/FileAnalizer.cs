using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CodeAnalizer.chuj;
namespace CodeAnalizer
{
    /// <summary>
    /// Class responsible for ghatering data from set of files. "Dataminer for multiple files" 
    /// and Pair filenames with stats.
    /// </summary>
    public class FileSetAnalizer
    {
        private DataMiner dataminer = new DataMiner(LanguageSelector.GetMethodTemlate(),LanguageSelector.GetNamePosition());
        private List<string> paths;

        public FileSetAnalizer()
        {
            this.paths = new List<string>();
        }

        public FileSetAnalizer(string[] paths)
        {
            this.paths = new List<string>();
            AddFiles(paths);
        }

        public void AddFiles(string[] paths)
        {
            List<string> tmp = new List<string>();
            foreach (var path in paths)
            {
                if (Directory.Exists(path))
                    tmp.AddRange(FindFilesInDirectory(path));
                else if (File.Exists(path))
                    tmp.Add(path);
            }

            this.paths.AddRange(tmp);
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
        /// <returns>String contains path to finded file or meesage saying that file doesnt exist</returns>
        public string GetLargestFile()
        {
            string ret=paths[0];
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
            string ret = paths[0];
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

        private string[] FindFilesInDirectory(string path)
        {
            Lister ls = new Lister(LanguageSelector.GetFileFormats());
            return ls.ListFiles(path);
        }
    }
}
