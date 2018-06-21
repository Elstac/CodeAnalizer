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

        /// <summary>
        /// Checks if given string is a method definition.
        /// </summary>
        /// <param name="text">Text to analize</param>
        /// <returns>True if text is method definition.</returns>
        public bool IsMethod(string text)
        {
            return IsMethodRec(text, 0);
        }
        /// <summary>
        /// Recursive method for determining if string is method definition. Uses templates given in constructor,
        /// in which last list is list of types of method definition part: #- required part, +- alternative part,
        /// others- part following method name.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="templateIndex"></param>
        /// <returns></returns>
        private bool IsMethodRec(string text, int templateIndex)
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

                tmp = StringEditor.GetNextWord(text);

                if (tmp == item)
                {
                       return IsMethodRec(text.Substring(tmp.Length), templateIndex + 1 -(alter?1:0));
                }
            }
            if (alter)
                return IsMethodRec(text, templateIndex + 1);
            return false;
        }
        /// <summary>
        /// Removes all characters in tring from beggining to given symbol( string).
        /// </summary>
        /// <param name="text"></param>
        /// <param name="nextSymbol">Symbol defining end of erasing</param>
        public void RemoveMethodName( ref string text,string nextSymbol)
        {
            int nextSymbolLenght = nextSymbol.Length, endIndex = 0;

            while (endIndex != text.Length - 1 && text.Substring(endIndex, nextSymbolLenght) != nextSymbol)
                endIndex++;

            text = text.Substring(endIndex);
        }
    }
}
