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
            string[] tmp = Directory.GetFiles(directory);
            List<string> ret = new List<string>();

            foreach (var path in tmp)
            {
                foreach (var format in allowedFormats)
                {
                    if (path.EndsWith(format))
                    {
                        ret.Add(path);
                    }
                }
            }

            return ret.ToArray();    
        }

       
    }
}
