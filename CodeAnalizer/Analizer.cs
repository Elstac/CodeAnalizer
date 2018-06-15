using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace CodeAnalizer
{
    /// <summary>
    /// Class responsible for ghatering data from set of files. "Dataminer for multiple files" and Pair filenames with stats.
    /// </summary>
    public class Analizer
    {
        private DataMiner dataminer = new DataMiner();
        private string[] paths;

        public Analizer(string[] paths)
        {
            this.paths=paths;
        }

        public string analizeFiles()
        {
            StringBuilder ret = new StringBuilder();
            foreach (var path in paths)
            {
                
                ret.Append(path+"\n");
                ret.Append("Lines: "+ dataminer.countLines(path));
                ret.Append("Empty Lines: "+dataminer.CountEmpty(path));
                ret.Append(" Characters: "+dataminer.countCharacters(path)+"\n");
            }
            return ret.ToString();
        }

        public int GetLinesCount()
        {
            int ret = 0;
            foreach (var path in paths)
            {
                ret += dataminer.countLines(path);
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
        public int GetCharacktersCount()
        {
            int ret = 0;
            foreach (var path in paths)
            {
                ret += dataminer.countCharacters(path);
            }
            return ret;
        }
        public int GetUsingsCount()
        {
            int ret = 0;
            foreach (var path in paths)
            {
                ret += dataminer.countUsings(path);
            }
            return ret;
        }

        public int GetCommentLines()
        {
            int ret = 0;
            foreach (var path in paths)
            {
                ret += dataminer.CountComment(path);
            }
            return ret;
        }
        public string GetLargestFile()
        {
            string ret="There is no largest file";
            int maxChar = 0,tmp;
            foreach (var path in paths)
            {
                tmp = dataminer.countCharacters(path);
                if (maxChar < tmp)
                {
                    maxChar = tmp;
                    ret = path;
                }
            }

            return ret;
        }
    }
}
