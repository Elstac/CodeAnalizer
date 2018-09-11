using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeAnalizer.FileAnalizerModule.Interfaces;
using System.IO;
namespace CodeAnalizer.FileAnalizerModule.Classes
{
    public class FileSetMiner:FileMiner
    {
        public FileSetMiner(string[] paths):base()
        {
            DataMiner tmp;

            foreach (var path in paths)
            {
                tmp = new DataMiner(LanguageSelector.GetMethodTemlate(), path);
                Children.Add(tmp);
            }
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
            DataMiner point;
            foreach (var path in paths)
            {
                point = new DataMiner(LanguageSelector.GetMethodTemlate(), path);
                Children.Add(point);
            }
        }

        public bool RemoveFile(string path)
        {

            if (Children.Count == 0)
                throw new EmptyAnalizerException();
            DataMiner tmp;
            foreach (var child in Children)
            {
                tmp = child as DataMiner;
                if (tmp.Path != path)
                    continue;
                Children.Remove(child);
                return true;
                
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
