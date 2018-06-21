using System;
using System.Collections.Generic;
using System.IO;
namespace CodeAnalizer
{
    /// <summary>
    /// Class responsible for listing files in allowed formats.
    /// </summary>
    public class Lister
    {
        private string[] allowedFormats;
        private string[] ignoreArray;
        public Lister(string[] formats)
        {
            allowedFormats = formats;
            ignoreArray = new string[] { };
        }
        public Lister(string[] formats, string[] ignore)
        {
            allowedFormats = formats;
            ignoreArray = ignore;
        }
        public string[] ListFiles(string directory)
        {
            string[] ret = ListFilesRec(directory);
            if (ret.Length == 0)
                throw new FileNotFoundException("No valid files found.");

            return ret;
        }



        private string[] ListFilesRec(string directory)
        {
            string[] tmp = Directory.GetFiles(directory);
            string[] dirs = Directory.GetDirectories(directory);

            List<string> ret = new List<string>();

            foreach (var path in tmp)
            {
                foreach (var format in allowedFormats)
                {
                    if (path.EndsWith(format))
                    {
                        ret.Add(path);
                        break;
                    }
                    
                }
            }
            foreach (var dir in dirs)
            {
                if (IsIgnored(dir))
                    continue;
                ret.AddRange(ListFilesRec(dir));
            }

            return ret.ToArray();    
        }

        private bool IsIgnored(string dir)
        {
            foreach (var item in ignoreArray)
                if (dir.EndsWith(item))
                    return true;

            return false;
        }
    }
}
