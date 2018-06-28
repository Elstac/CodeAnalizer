using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalizer
{
    interface IFileSaver
    {
        void SaveFileState(string name, string path);

        void SaveDirectory(string name, string path);

        void SaveFileChange(Changes fileChanges);

        void UpdateFiles(Changes[] filesChanges);

        void SaveData(string name, AData data);
    }
}
