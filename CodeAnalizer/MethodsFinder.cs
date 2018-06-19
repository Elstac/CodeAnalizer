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
        private int methodName;
        public MethodsFinder(List<string>[] templates, int methodName)
        {
            this.templates = templates;
            this.methodName = methodName;
        }

        public bool IsMethod(string text, int templateIndex)
        {
            text = StringEditor.GetRawText(text);
            if (text.Length < 6)
                return false;

            string tmp;
            foreach (var item in templates[templateIndex])
            {
                if (templateIndex == methodName)
                    RemoveMethodName(ref text);

                tmp = text.Substring(0, item.Length);
                if (tmp == item)
                {
                    if (templateIndex == templates.Length - 1)
                        return true;
                    else
                       return IsMethod(text.Substring(tmp.Length), templateIndex + 1);

                }
            }
            return false;
        }

        public void RemoveMethodName( ref string text)
        {
            string nextSymbol = templates[methodName][0];
            int nextSymbolLenght = nextSymbol.Length, endIndex = 0;

            while (endIndex != text.Length - 1 && text.Substring(endIndex, nextSymbolLenght) != nextSymbol)
                endIndex++;

            text = text.Substring(endIndex);
        }
    }
}
