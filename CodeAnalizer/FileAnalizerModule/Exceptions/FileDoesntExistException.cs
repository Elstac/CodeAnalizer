using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalizer
{
    class FileDoesntExistException: Exception
    {
        List<string> files;
        List<string> Files { get =>  files; }
        public FileDoesntExistException(string[] paths)
        {
            files = new List<string>();
            Files.AddRange(paths);

            string msg = "File";
            if (paths.Length <= 0)
                msg = "All files exist. There is no problem";
            else
            {
                msg += paths.Length > 1 ? "s: " : ": ";
                foreach (var path in paths)
                {
                    msg += path + ", ";
                }
                msg = msg.Substring(0, msg.Length - 2);
                msg += " not exist";
            }
        }
        private FileDoesntExistException(string msg,List<string>paths) : base(msg)
        {
            files = paths;
        }

    }
}
