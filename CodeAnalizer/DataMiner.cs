using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace CodeAnalizer
{
    public enum CommentType
    {
        NoComment,
        Normal,
        MultipleBegin,
        MultipleEnd
    }
    /// <summary>
    /// Class responsible for parsing data from given file.
    /// </summary>
    public class DataMiner
    {
        private MethodsFinder finder;

        public DataMiner(List<string>[] templates, int nameIndex)
        {
            finder = new MethodsFinder(templates, nameIndex);
        }
        /// <summary>
        /// Counts number of lines( empty or not) in given file.
        /// </summary>
        /// <param name="path">Path to file</param>
        /// <returns>Number of lines in given file</returns>
        public int countLines(string path)
        {
            FileStream file = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(file);
            int ret = 0;
            string line;
            while ((line = sr.ReadLine()) != null) {
                line =StringEditor.GetRawText(line);
                if (line == "")
                    continue;

                ret++;
            }
            file.Close();
            return ret;
        }
        /// <summary>
        /// Counts empty lines in given file.
        /// </summary>
        /// <param name="path">Path to the file.</param>
        /// <returns>Number of empty lines if file.</returns>
        public int CountEmpty(string path)
        {
            FileStream file = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(file);
            int ret = 0;
            string line;
            while ((line = sr.ReadLine()) != null)
            {   
                line =StringEditor.GetRawText(line);
                if(line=="")
                    ret++;
            }
            file.Close();
            return ret;
        }
        public int countUsings(string path)
        {
            FileStream file = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(file);
            int ret = 0;
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                line =StringEditor.GetRawText(line);
                if (line.Length >= 5)
                    if (line.Substring(0, 5) == "using")
                        ret++;
            }
            file.Close();
            return ret;
        }
        /// <summary>
        /// Counts all characters in given file.
        /// </summary>
        /// <param name="path">Path to the file</param>
        /// <returns>Number of characters in file</returns>
        public int countCharacters(string path)
        {
            int ret = 0;
            FileStream file = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(file);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                line =StringEditor.GetRawText(line);
                ret += line.Length;
            }
            file.Close();
            return ret;
        }

        public int CountComment(string path)
        {
            int ret = 0;
            FileStream file = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(file);
            string line;
            bool coment = false;
            while ((line = sr.ReadLine()) != null)
            {
                line =StringEditor.GetRawText(line);
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

        public int CountMethods(string path)
        {
            int ret = 0;
            FileStream file = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(file);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                line =StringEditor.GetRawText(line);
                if (finder.IsMethod(line,0))
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
            if(tmp=="/*")
                return CommentType.MultipleBegin;
            if (text.EndsWith("*/"))
                return CommentType.MultipleEnd;
            return CommentType.NoComment;
        }

        private bool IsMethod(string text, int templateIndex)
        {
            text = StringEditor.GetRawText(text);
            string tmp;
            foreach (var item in methodTemplates[templateIndex])
            {
                tmp = text.Substring(0, item.Length);
                if (tmp == item)
                {
                    if (templateIndex == methodTemplates.Length - 1)
                        return true;
                    else
                        IsMethod(tmp, templateIndex + 1);
                        
                }
            }
            return false;
        }
    }
}
