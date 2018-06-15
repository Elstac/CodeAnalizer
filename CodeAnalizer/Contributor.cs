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
        Analizer analizer;
        public string Name { get => _name; }
        public Analizer Analizer { get => analizer; }
        public float WasteParam { get => _wasteParam; set => _wasteParam = value; }

        public Contributor(string name, Analizer analizer)
        {
            _name = name;
            this.analizer = analizer;
            WasteParam = (((float)analizer.GetEmptyLines() / analizer.GetLinesCount())*100);
        }

    }
}
