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
        private string pathToImage;
        private float _wasteParam;
        FileSetAnalizer analizer;
        public string Name { get => _name; set => _name = value; }
        public FileSetAnalizer Analizer { get => analizer; }
        public float WasteParam { get => _wasteParam; set => _wasteParam = value; }
        public string PathToImage { get => pathToImage; set => pathToImage = value; }

        public Contributor(string name)
        {
            Name = name;
            WasteParam = 0;
            
        }
        public Contributor(string name, FileSetAnalizer analizer)
        {
            _name = name;
            this.analizer = analizer;
            WasteParam = (((float)analizer.GetEmptyLines() / analizer.GetLinesCount())*100);
        }

        public Contributor(string name,string pathToImage ,FileSetAnalizer analizer)
        {
            _name = name;
            PathToImage = pathToImage;
            this.analizer = analizer;
            WasteParam = (((float)analizer.GetEmptyLines() / analizer.GetLinesCount()) * 100);
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
