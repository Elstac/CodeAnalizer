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
            List<List<string>> ret= null;
            if (language == Language.Csharp)
            {
                ret = new List<List<string>>
                {
                    new List<string>
                    {
                        "public",
                        "private",
                        "protected"
                    },
                    new List<string>
                    {
                        "static",
                        "abstract",
                        "override",
                        "seald"
                    },
                    new List<string>
                    {
                        "void",
                        "int",
                        "float",
                        "double",
                        "bool",
                        "long"
                    },
                    new List<string>
                    {
                        "#",
                        "+",
                        "#",
                        "("
                    },
                };
            }
            else
                throw new NotImplementedException();
            return ret.ToArray();
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
