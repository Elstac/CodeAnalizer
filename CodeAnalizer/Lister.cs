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
        }
        public Lister(string[] formats, string[] ignore)
        {
            allowedFormats = formats;
            ignoreArray = ignore;
        }
        public string[] ListFiles(string directory)
        {
            return ListFilesRec(directory);
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
                ret.AddRange(ListFiles(dir));
            }
            if (ret.Count == 0)
                throw new FileNotFoundException("No valid files found.");

            return ret.ToArray();    
        }

        private bool IsIgnored(string dir)
        {
            foreach (var item in ignoreArray)
                if (dir == item)
                    return true;

            return false;
        }
    }
}
