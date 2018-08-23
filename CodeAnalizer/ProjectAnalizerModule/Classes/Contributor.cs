using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalizer
{
    /// <summary>
    /// Class represents contributor to project. Stores name and codes statistics.
    /// </summary>
    public class Contributor
    {
        private string _name;
        private float _wasteParam;
        FileAnalizer analizer;
        public string Name { get => _name; set => _name = value; }
        public FileAnalizer Analizer { get => analizer; }
        public float WasteParam { get => _wasteParam; set => _wasteParam = value; }

        public Contributor(string name)
        {
            Name = name;
            WasteParam = 0;
        }
        public Contributor(string name, FileAnalizer analizer)
        {
            _name = name;
            this.analizer = analizer;
            WasteParam = (((float)analizer.GetEmptyLines() / analizer.GetLinesCount())*100);
        }

        public void AddFiles(string paths)
        {

        }
    }
}
