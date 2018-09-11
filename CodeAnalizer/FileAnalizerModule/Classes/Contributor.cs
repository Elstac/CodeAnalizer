using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CodeAnalizer.FileAnalizerModule.Classes;
namespace CodeAnalizer
{
    /// <summary>
    /// Class represents contributor to project. Stores name and codes statistics, manage added and removed files in analizer
    /// </summary>
    public class Contributor
    {
        private string _name;
        private float _wasteParam;
        FileSetMiner analizer;
        public string Name { get => _name; set => _name = value; }
        public FileSetMiner Analizer { get => analizer; }
        public float WasteParam { get => _wasteParam; set => _wasteParam = value; }

        public Contributor(string name)
        {
            Name = name;
            WasteParam = 0;
            
        }
        public Contributor(string name, FileSetMiner analizer)
        {
            _name = name;
            this.analizer = analizer;
            WasteParam = (((float)analizer.GetEmptyLines() / analizer.GetLinesCount())*100);
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
            
            analizer.AddFiles(tmp.ToArray());
        }
        private string[] FindFilesInDirectory(string path)
        {
            Lister ls = new Lister(LanguageSelector.GetFileFormats());
            return ls.ListFiles(path);
        }

        public void RemoveFiles(string[] paths)
        {
            List<string> missing = new List<string>();
            foreach (var path in paths)
            {
                    if (!analizer.RemoveFile(path))
                        missing.Add(path);
            }
            if (missing.Count != 0)
                throw new FileDoesntExistException(missing.ToArray());
        }
    }
}
