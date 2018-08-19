using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalizer
{
    public static class StringEditor
    {
        /// <summary>
        /// Returns raw text in string( without beginig empty characters).
        /// </summary>
        /// <param name="text">Base string</param>
        /// <returns>String that contain raw text</returns>
        public static string GetRawText(string text)
        {
            string ret = text;
            int tmp = 0;

            while (tmp < ret.Length && ret[tmp] == ' ')
                tmp++;

            ret = text.Substring(tmp);

            return ret;
        }

        public static string GetNextWord(string text)
        {
            int index = 0;
            text = GetRawText(text);

            while (index!=text.Length&& text[index] != ' ')
                index++;

            return text.Substring(0, index);
        }

        public static List<string> GetLines(string text)
        {
            List<string> ret = new List<string>();
            int posB=0,posE;
            string tmp;
            while(text.Length >0)
            {
                posE = text.IndexOf('\n', posB);
                if (posE == -1)
                    break;
                tmp = text.Substring(posB, posE-posB);
                ret.Add(tmp);
                posB = posE+1;
            }
            return ret;
        }
    }
}
