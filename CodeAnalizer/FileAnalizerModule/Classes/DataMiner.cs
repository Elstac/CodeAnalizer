using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeAnalizer.FileAnalizerModule.Interfaces;
using System.IO;
namespace CodeAnalizer.FileAnalizerModule.Classes
{
    public class DataMiner : IFileMiner
    {
        private string path;
        private MethodsFinder finder;
        private bool opendComment = false;
        public DataMiner(List<string>[] templates,string path)
        {
            this.path = path;
            finder = new MethodsFinder(templates);
        }
        public int GetCharactersCount()
        {
            throw new NotImplementedException();
        }

        public int GetCommentLines()
        {
            throw new NotImplementedException();
        }

        public int GetEmptyLines()
        {
            throw new NotImplementedException();
        }

        public Tuple<int, string> GetLargestFile()
        {
            throw new NotImplementedException();
        }

        public int GetLinesCount()
        {
            FileStream file = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(file);
            int ret = 0;
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                line = StringEditor.GetRawText(line);
                if (line == "")
                    continue;

                ret++;
            }
            file.Close();
            return ret;
        }

        public int GetMethodsCount()
        {
            throw new NotImplementedException();
        }

        public Tuple<int, string> GetSmallestFile()
        {
            throw new NotImplementedException();
        }

        public int GetUsingsCount()
        {
            throw new NotImplementedException();
        }
    }
}
