using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeAnalizer.FileAnalizerModule.Interfaces;
using System.IO;
namespace CodeAnalizer.FileAnalizerModule.Classes
{
    public enum CommentType
    {
        NoComment,
        Normal,
        MultipleBegin,
        MultipleEnd
    }
    public class DataMiner : IFileMiner
    {
        private string path;
        private MethodsFinder finder;

        public string Path { get => path; set => path = value; }

        public DataMiner(List<string>[] templates,string path)
        {
            this.path = path;
            finder = new MethodsFinder(templates);
        }
        public int GetCharactersCount()
        {
            int ret = 0;
            FileStream file = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(file);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                line = StringEditor.GetRawText(line);
                ret += line.Length;
            }
            file.Close();
            return ret;
        }

        public int GetCommentLines()
        {
            int ret = 0;
            FileStream file = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(file);
            string line;
            bool coment = false;
            while ((line = sr.ReadLine()) != null)
            {
                line = StringEditor.GetRawText(line);
                CommentType tmp = TypeOfComment(line);

                if (coment)
                    ret++;
                if (tmp == CommentType.NoComment)
                    continue;

                if (tmp == CommentType.MultipleEnd && coment)
                {
                    coment = false;
                    continue;
                }
                else if (tmp == CommentType.MultipleBegin)
                    coment = true;
                ret++;
            }
            file.Close();
            return ret;
        }

        private CommentType TypeOfComment(string text)
        {
            if (text.Length < 2)
                return CommentType.NoComment;

            string tmp = text.Substring(0, 2);
            if (tmp == "//")
                return CommentType.Normal;
            if (tmp == "/*")
                return CommentType.MultipleBegin;
            if (text.EndsWith("*/"))
                return CommentType.MultipleEnd;
            return CommentType.NoComment;
        }

        public int GetEmptyLines()
        {
            Func<string, bool> condition = delegate (string s)
            {
                return (s == "");
            };
            return AddLines(condition);
        }

        public Tuple<int, string> GetLargestFile()
        {
            throw new NotImplementedException();
        }

        public int GetLinesCount()
        {
            Func<string, bool> condition = delegate (string s)
             {
                 return (s != "");
             };
            return AddLines(condition);
        }

        private int AddLines(Func<string, bool> condition)
        {
            FileStream file = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(file);
            int ret = 0;
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                line = StringEditor.GetRawText(line);
                if (condition(line))
                    ret++;
            }
            file.Close();
            return ret;
        }
        public int GetMethodsCount()
        {
            int ret = 0;
            FileStream file = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(file);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                line = StringEditor.GetRawText(line);
                if (finder.IsMethod(line))
                    ret++;
            }

            file.Close();
            return ret;
        }

        public Tuple<int, string> GetSmallestFile()
        {
            throw new NotImplementedException();
        }

        public int GetUsingsCount()
        {
            Func<string, bool> condition = delegate (string s)
             {
                 return (s.Length >= 5 && s.Substring(0, 5) == "using");
             };
            return AddLines(condition);
        }
    }
}
