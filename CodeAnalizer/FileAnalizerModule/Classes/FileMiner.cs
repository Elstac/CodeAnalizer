using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeAnalizer.FileAnalizerModule.Interfaces;
namespace CodeAnalizer.FileAnalizerModule.Classes
{
    public abstract class FileMiner : IFileMiner
    {
        private List<IFileMiner> children;
        public FileMiner()
        {
            children = new List<IFileMiner>();
        }

        public List<IFileMiner> Children { get => children;}

        public int GetCharactersCount()
        {
            int ret = 0;
            foreach (var child in children)
                ret += child.GetCharactersCount();
            return ret;
        }

        public int GetCommentLines()
        {
            int ret = 0;
            foreach (var child in children)
                ret += child.GetCommentLines();
            return ret;
        }

        public int GetEmptyLines()
        {
            int ret = 0;
            foreach (var child in children)
                ret += child.GetEmptyLines();
            return ret;
        }

        public Tuple<int, string> GetLargestFile()
        {
            string path = ""; int maxSize = 0;
            foreach (var child in children)
            {
                Tuple<int, string> tmp = child.GetLargestFile();
                if (tmp.Item1 >= maxSize)
                { 
                    path = tmp.Item2;
                    maxSize = tmp.Item1;
                }


            }
            return new Tuple<int, string>(maxSize, path);
        }

        public int GetLinesCount()
        {
            int ret = 0;
            foreach (var child in children)
                ret += child.GetLinesCount();
            return ret;
        }

        public int GetMethodsCount()
        {
            int ret = 0;
            foreach (var child in children)
                ret += child.GetMethodsCount();
            return ret;
        }

        public Tuple<int, string> GetSmallestFile()
        {
            string path = ""; int minSize = 1000000;
            foreach (var child in children)
            {
                Tuple<int, string> tmp = child.GetSmallestFile();
                if (tmp.Item1 <= minSize)
                {
                    path = tmp.Item2;
                    minSize = tmp.Item1;
                }

            }
            return new Tuple<int, string>(minSize, path);
        }

        public int GetUsingsCount()
        {
            int ret = 0;
            foreach (var child in children)
                ret += child.GetUsingsCount();
            return ret;
        }
    }
}
