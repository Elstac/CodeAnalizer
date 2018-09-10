using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace CodeAnalizer
{
    /// <summary>
    /// Class responsible for managing FileAnalizers set. Res: adding new analizers, returning analizers list
    /// removing file from all analizers,
    /// </summary>
    public class FileManager
    {
        private List<FileSetAnalizer> _analizers;
        Lister fileLister;
        public List<FileSetAnalizer> Analizers { get => _analizers;}

        public FileManager(string[]paths, Language lan)
        {
            LanguageSelector.Language = lan;
            _analizers = new List<FileSetAnalizer>();

            fileLister = new Lister(LanguageSelector.GetFileFormats());
            AddFilesGroup(paths);
        }

        public void AddFilesGroup(string[] paths)
        {
            List<string> toAdd = new List<string>();
            foreach (var path in paths)
            {
                if (Directory.Exists(path))
                {
                    toAdd.AddRange(fileLister.ListFiles(path));
                }
                else
                    toAdd.Add(path);
            }
            Analizers.Add(new FileSetAnalizer(toAdd.ToArray()));
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
