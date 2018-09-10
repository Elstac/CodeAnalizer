using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeAnalizer.FileAnalizerModule.Interfaces;
namespace CodeAnalizer.FileAnalizerModule.Classes
{
    class FileSetMiner:FileMiner
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
    }
}
