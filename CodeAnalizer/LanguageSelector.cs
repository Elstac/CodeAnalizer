using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalizer
{
    public enum Language
    {
        Csharp,
        CPP,
        Java
    }
    public class LanguageSelector
    {
        private static Language language;

        public static Language Language { get => language; set => language = value; }

        public static List<string>[] GetMethodTemlate()
        {
            List<string>[] ret= null;
            if (language == Language.Csharp)
            {
                ret = new List<string>[3];
                ret[0] = new List<string>();
                ret[0].AddRange(new string[] { "public", "private", "protected" });
                ret[1] = new List<string>();
                ret[1].AddRange(new string[] { "void", "int", "float", "bool", "string" });
                ret[2] = new List<string>();
                ret[2].AddRange(new string[] { "(" });
            }
            else
                throw new NotImplementedException();
            return ret;
        }
        public static int GetNamePosition()
        {
            if (language == Language.Csharp)
            {
                return 2;
            }
            else throw new NotImplementedException();
        }

        public string[] GetFileFormats()
        {
            string[] ret = null;
            if (language == Language.Csharp)
            {
                ret = new string[1];
                ret[0] = ".cs";
            }
            return ret;
        }
    }

}
