using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace CodeAnalizer
{
    class FileManager
    {
        private List<FileAnalizer> _analizers;

        public List<FileAnalizer> Analizers { get => _analizers;}

        public FileManager(string[]paths)
        {
            _analizers = new List<FileAnalizer>();
            _analizers.Add(new FileAnalizer(paths));

        }

        public void AddFilesGroup(string[] paths)
        {
            Analizers.Add(new FileAnalizer(paths));
        }

        public void RemoveFiles(string path)
        {
            bool succes = false;
            foreach (var analizer in Analizers)
            {
                try
                {
                    if (analizer.RemoveFile(path))
                    {
                        succes = true;
                        break;
                    }

                }
                catch (EmptyAnalizerException)
                {
                    Analizers.Remove(analizer);
                    break;
                }
            }
            if(!succes)
                throw new FileNotFoundException("File: " + path + " doesnt exist or has been deleted already");
        }
    }
}
