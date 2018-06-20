using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalizer
{
    /// <summary>
    /// Class responsible for cheking if strings contains declaration of methods.
    /// </summary>
    public class MethodsFinder
    {
        private List<string>[] templates;
        public MethodsFinder(List<string>[] templates)
        {
            this.templates = templates;
            
        }

        public bool IsMethod(string text, int templateIndex)
        {
            
            text = StringEditor.GetRawText(text);
            string tmp, type;
            bool alter = false;
            type = templates[templates.Length - 1][templateIndex];

            if (type != "#" && type != "+")
            {
                try
                {
                    RemoveMethodName(ref text, type);
                    return (text.Substring(0, type.Length) == type);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    return false;
                }
            }

            alter = (type == "+");
            foreach (var item in templates[templateIndex])
            {
                if (text.Length < item.Length)
                    return false;

                tmp = text.Substring(0, item.Length);
                if (tmp == item)
                {
                       return IsMethod(text.Substring(tmp.Length), templateIndex + 1 +(alter?1:0));
                }
            }
            if (alter)
                return IsMethod(text, templateIndex + 1);
            return false;
        }

        public void RemoveMethodName( ref string text,string nextSymbol)
        {
            int nextSymbolLenght = nextSymbol.Length, endIndex = 0;

            while (endIndex != text.Length - 1 && text.Substring(endIndex, nextSymbolLenght) != nextSymbol)
                endIndex++;

            text = text.Substring(endIndex);
        }
    }
}
